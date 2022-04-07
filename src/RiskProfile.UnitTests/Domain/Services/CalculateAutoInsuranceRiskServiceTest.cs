using Moq;
using RiskProfile.CalculatorEngine;
using RiskProfile.Domain.Interfaces;
using RiskProfile.Domain.Interfaces.Specifications;
using RiskProfile.Domain.Models;
using RiskProfile.Domain.Models.PersonalProfileInformation;
using RiskProfile.Domain.Services;
using RiskProfile.UnitTests.Fixtures;
using System;
using Xunit;

namespace RiskProfile.UnitTests.Domain.Services
{
    public class CalculateAutoInsuranceRiskServiceTest
    {
        private readonly IRiskScoreCalculatorBuilder<PersonalProfile> riskScoreCalculatorBuilder;
        private readonly Mock<INewVehicleSpecification> newVehicleSpecification;
        private readonly Mock<IGeneralRiskScoreCalculator<PersonalProfile>> generalRiskScoreCalculator;

        private readonly CalculateAutoInsuranceRiskService calculator;

        public CalculateAutoInsuranceRiskServiceTest()
        {
            newVehicleSpecification = new();
            riskScoreCalculatorBuilder = new RiskScoreCalculatorBuilder<PersonalProfile>();
            
            generalRiskScoreCalculator = new();
            generalRiskScoreCalculator
                .Setup(p => p.Calculate(It.IsAny<PersonalProfile>()))
                .Returns(new RiskScore());

            calculator = new(riskScoreCalculatorBuilder,
                newVehicleSpecification.Object,
                generalRiskScoreCalculator.Object);
        }

        [Fact]
        public void Should_TakeInConsiderationGeneralRiskCalculators_AddGeneralRiskCalculatorsToItself()
        {
            // Arrange
            var riskScoreCalculatorBuilder = new Mock<IRiskScoreCalculatorBuilder<PersonalProfile>>();

            riskScoreCalculatorBuilder
                .Setup(p => p.AddCalculator(It.IsAny<Func<PersonalProfile, bool>>(), It.IsAny<RiskScore>()))
                .Returns(riskScoreCalculatorBuilder.Object);

            riskScoreCalculatorBuilder
                .Setup(p => p.StartBuilding())
                .Returns(riskScoreCalculatorBuilder.Object);

            riskScoreCalculatorBuilder
                .Setup(p => p.AddCalculator(It.IsAny<IRiskScoreCalculator<PersonalProfile>>()))
                .Returns(riskScoreCalculatorBuilder.Object);

            // Act
            var calculator = new CalculateAutoInsuranceRiskService(riskScoreCalculatorBuilder.Object,
                newVehicleSpecification.Object,
                generalRiskScoreCalculator.Object);

            // Assert
            riskScoreCalculatorBuilder.Verify(p => p.AddCalculator(It.IsAny<IGeneralRiskScoreCalculator<PersonalProfile>>()), Times.Once);
        }

        [Fact]
        public void CalculateRisk_Should_ConsiderAProfileWithoutCarAsIneligible()
        {
            // Arrange
            PersonalProfile profile = PersonalProfileFixture.CreatePersonalProfile();
            profile.Vehicle = null;

            // Act
            var risk = calculator.CalculateRisk(profile);

            // Assert
            Assert.False(risk.Eligible);
        }

        [Fact]
        public void CalculateRisk_Should_GiveTheRightRiskForANewCar()
        {
            // Arrange
            PersonalProfile profile = PersonalProfileFixture.CreatePersonalProfile();
            newVehicleSpecification
                .Setup(p => p.IsANewVehicle(profile.Vehicle))
                .Returns(true);

            // Act
            var risk = calculator.CalculateRisk(profile);

            // Assert
            Assert.Equal(1, risk.Score);
        }

        [Fact]
        public void CalculateRisk_Should_GiveTheRightRiskForAnOldCar()
        {
            // Arrange
            PersonalProfile profile = PersonalProfileFixture.CreatePersonalProfile();
            newVehicleSpecification
                .Setup(p => p.IsANewVehicle(profile.Vehicle))
                .Returns(false);

            // Act
            var risk = calculator.CalculateRisk(profile);

            // Assert
            Assert.Equal(0, risk.Score);
        }
    }
}
