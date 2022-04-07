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
    public class CalculateHomeInsuranceRiskServiceTest
    {
        public readonly IRiskScoreCalculatorBuilder<PersonalProfile> riskScoreCalculatorBuilder;
        public readonly Mock<IMortgagedHouseSpecification> mortgagedHouseSpecification;
        public readonly Mock<IHasDependentsSpecification> hasDependentsSpecification;
        private readonly Mock<IGeneralRiskScoreCalculator<PersonalProfile>> generalRiskScoreCalculator;

        public readonly CalculateHomeInsuranceRiskService calculator;

        public CalculateHomeInsuranceRiskServiceTest()
        {
            riskScoreCalculatorBuilder = new RiskScoreCalculatorBuilder<PersonalProfile>();
            mortgagedHouseSpecification = new();
            hasDependentsSpecification = new ();

            generalRiskScoreCalculator = new();
            generalRiskScoreCalculator
                .Setup(p => p.Calculate(It.IsAny<PersonalProfile>()))
                .Returns(new RiskScore());

            calculator = new(
                riskScoreCalculatorBuilder,
                mortgagedHouseSpecification.Object,
                generalRiskScoreCalculator.Object);
        }

        [Fact]
        public void CalculateRisk_Should_ClassifyProfileAsIneligible_When_ProfileDoesntPresentAHouse()
        {
            // Arrange
            PersonalProfile personalProfile = PersonalProfileFixture.CreatePersonalProfile();
            personalProfile.House = null;

            mortgagedHouseSpecification.Setup(p => p.IsMortgaged(It.IsAny<House>())).Returns(false);
            hasDependentsSpecification.Setup(p => p.HasDependents(It.IsAny<PersonalProfile>())).Returns(false);

            // Act
            var risk = calculator.CalculateRisk(personalProfile);

            // Assert
            Assert.False(risk.Eligible);
        }

        [Fact]
        public void CalculateRisk_Should_GiveTheRightRisk_When_TheHouseIsMortgaged()
        {
            // Arrange
            PersonalProfile personalProfile = PersonalProfileFixture.CreatePersonalProfile();

            mortgagedHouseSpecification.Setup(p => p.IsMortgaged(It.IsAny<House>())).Returns(true);
            hasDependentsSpecification.Setup(p => p.HasDependents(It.IsAny<PersonalProfile>())).Returns(false);

            // Act
            var risk = calculator.CalculateRisk(personalProfile);

            // Assert
            Assert.Equal(1, risk.Score);
        }

        [Fact]
        public void Should_ConsiderTheRiskOfGeneralCalculators()
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
            var calculator = new CalculateHomeInsuranceRiskService(
                riskScoreCalculatorBuilder.Object,
                mortgagedHouseSpecification.Object,
                generalRiskScoreCalculator.Object);

            // Assert
            riskScoreCalculatorBuilder.Verify(p => p.AddCalculator(It.IsAny<IGeneralRiskScoreCalculator<PersonalProfile>>()), Times.Once);
        }
    }
}
