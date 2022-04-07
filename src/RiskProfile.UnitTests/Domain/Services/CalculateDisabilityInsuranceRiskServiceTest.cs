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
    public class CalculateDisabilityInsuranceRiskServiceTest
    {
        private readonly Mock<IBeingOldSpecification> beingOldSpecification;
        private readonly Mock<IMortgagedHouseSpecification> mortgagedHouseSpecification;
        private readonly Mock<IHasDependentsSpecification> hasDependentsSpecification;
        private readonly Mock<IBeingMarriedSpecification> beingMarriedSpecification;
        private readonly Mock<IGeneralRiskScoreCalculator<PersonalProfile>> generalRiskScoreCalculator;
        private readonly IRiskScoreCalculatorBuilder<PersonalProfile> riskScoreCalculatorBuilder;

        private readonly CalculateDisabilityInsuranceRiskService calculator;

        public CalculateDisabilityInsuranceRiskServiceTest()
        {
            riskScoreCalculatorBuilder = new RiskScoreCalculatorBuilder<PersonalProfile>();
            beingOldSpecification = new();
            mortgagedHouseSpecification = new();
            hasDependentsSpecification = new();
            beingMarriedSpecification = new();

            generalRiskScoreCalculator = new();
            generalRiskScoreCalculator
                .Setup(p => p.Calculate(It.IsAny<PersonalProfile>()))
                .Returns(new RiskScore());

            calculator = new(
                riskScoreCalculatorBuilder,
                beingOldSpecification.Object,
                mortgagedHouseSpecification.Object,
                hasDependentsSpecification.Object,
                beingMarriedSpecification.Object,
                generalRiskScoreCalculator.Object);
        }

        [Fact]
        public void Should_ConsiderTheGeneralCalculatorsRisk()
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
            var calculator = new CalculateDisabilityInsuranceRiskService(riskScoreCalculatorBuilder.Object,
                beingOldSpecification.Object,
                mortgagedHouseSpecification.Object,
                hasDependentsSpecification.Object,
                beingMarriedSpecification.Object,
                generalRiskScoreCalculator.Object);

            // Assert
            riskScoreCalculatorBuilder.Verify(p => p.AddCalculator(It.IsAny<IGeneralRiskScoreCalculator<PersonalProfile>>()), Times.Once);
        }

        [Fact]
        public void CalculateRisk_Should_ReturnAIneligibleScore_When_ProfileHasNoIncome()
        {
            // Arrange
            PersonalProfile profile = PersonalProfileFixture.CreatePersonalProfile();
            profile.Income = 0;

            beingOldSpecification.Setup(p => p.IsAnOldPerson(It.IsAny<PersonalProfile>())).Returns(false);
            beingMarriedSpecification.Setup(p => p.IsMarried(It.IsAny<PersonalProfile>())).Returns(false);
            mortgagedHouseSpecification.Setup(p => p.IsMortgaged(It.IsAny<House>())).Returns(false);
            hasDependentsSpecification.Setup(p => p.HasDependents(It.IsAny<PersonalProfile>())).Returns(false);

            // Act
            var risk = calculator.CalculateRisk(profile);

            // Assert
            Assert.False(risk.Eligible);
        }

        [Fact]
        public void CalculateRisk_Should_ReturnAIneligibleScore_When_ThePersonIsOld()
        {
            // Arrange
            PersonalProfile profile = PersonalProfileFixture.CreatePersonalProfile();
            profile.Income = 1;

            beingOldSpecification.Setup(p => p.IsAnOldPerson(It.IsAny<PersonalProfile>())).Returns(true);
            beingMarriedSpecification.Setup(p => p.IsMarried(It.IsAny<PersonalProfile>())).Returns(false);
            mortgagedHouseSpecification.Setup(p => p.IsMortgaged(It.IsAny<House>())).Returns(false);
            hasDependentsSpecification.Setup(p => p.HasDependents(It.IsAny<PersonalProfile>())).Returns(false);

            // Act
            var risk = calculator.CalculateRisk(profile);

            // Assert
            Assert.False(risk.Eligible);
        }

        [Fact]
        public void CalculateRisk_Should_ReturnTheRightRiskScore_When_ThePersonsHouseIsMortgaged()
        {
            // Arrange
            PersonalProfile profile = PersonalProfileFixture.CreatePersonalProfile();

            profile.Income = 1;
            beingOldSpecification.Setup(p => p.IsAnOldPerson(It.IsAny<PersonalProfile>())).Returns(false);
            beingMarriedSpecification.Setup(p => p.IsMarried(It.IsAny<PersonalProfile>())).Returns(false);
            mortgagedHouseSpecification.Setup(p => p.IsMortgaged(It.IsAny<House>())).Returns(true);
            hasDependentsSpecification.Setup(p => p.HasDependents(It.IsAny<PersonalProfile>())).Returns(false);

            // Act
            var risk = calculator.CalculateRisk(profile);

            // Assert
            Assert.Equal(1, risk.Score);
        }

        [Fact]
        public void CalculateRisk_Should_ReturnTheRightRiskScore_When_ThePersonHasDependents()
        {
            // Arrange
            PersonalProfile profile = PersonalProfileFixture.CreatePersonalProfile();

            profile.Income = 1;
            beingOldSpecification.Setup(p => p.IsAnOldPerson(It.IsAny<PersonalProfile>())).Returns(false);
            beingMarriedSpecification.Setup(p => p.IsMarried(It.IsAny<PersonalProfile>())).Returns(false);
            mortgagedHouseSpecification.Setup(p => p.IsMortgaged(It.IsAny<House>())).Returns(false);
            hasDependentsSpecification.Setup(p => p.HasDependents(It.IsAny<PersonalProfile>())).Returns(true);

            // Act
            var risk = calculator.CalculateRisk(profile);

            // Assert
            Assert.Equal(1, risk.Score);
        }

        [Fact]
        public void CalculateRisk_Should_ReturnTheRightRiskScore_When_ThePersonIsMarried()
        {
            // Arrange
            PersonalProfile profile = PersonalProfileFixture.CreatePersonalProfile();

            profile.Income = 1;
            beingOldSpecification.Setup(p => p.IsAnOldPerson(It.IsAny<PersonalProfile>())).Returns(false);
            beingMarriedSpecification.Setup(p => p.IsMarried(It.IsAny<PersonalProfile>())).Returns(true);
            mortgagedHouseSpecification.Setup(p => p.IsMortgaged(It.IsAny<House>())).Returns(false);
            hasDependentsSpecification.Setup(p => p.HasDependents(It.IsAny<PersonalProfile>())).Returns(false);

            // Act
            var risk = calculator.CalculateRisk(profile);

            // Assert
            Assert.Equal(-1, risk.Score);
        }
    }
}
