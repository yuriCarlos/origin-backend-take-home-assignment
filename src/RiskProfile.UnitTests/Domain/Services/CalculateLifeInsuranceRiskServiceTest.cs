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
    public class CalculateLifeInsuranceRiskServiceTest
    {

        public readonly IRiskScoreCalculatorBuilder<PersonalProfile> riskScoreCalculatorBuilder;
        public readonly Mock<IBeingOldSpecification> beingOldSpecification;
        public readonly Mock<IHasDependentsSpecification> hasDependentsSpecification;
        public readonly Mock<IBeingMarriedSpecification> beingMarriedSpecification;
        private readonly Mock<IGeneralRiskScoreCalculator<PersonalProfile>> generalRiskScoreCalculator;

        public readonly CalculateLifeInsuranceRiskService calculator;

        public CalculateLifeInsuranceRiskServiceTest()
        {
            riskScoreCalculatorBuilder = new RiskScoreCalculatorBuilder<PersonalProfile>();

            generalRiskScoreCalculator = new();
            generalRiskScoreCalculator
                .Setup(p => p.Calculate(It.IsAny<PersonalProfile>()))
                .Returns(new RiskScore());

            beingOldSpecification = new();
            beingOldSpecification.Setup(x => x.IsAnOldPerson(It.IsAny<PersonalProfile>())).Returns(false);

            hasDependentsSpecification = new();
            hasDependentsSpecification.Setup(x => x.HasDependents(It.IsAny<PersonalProfile>())).Returns(false);

            beingMarriedSpecification = new();
            beingMarriedSpecification.Setup(x => x.IsMarried(It.IsAny<PersonalProfile>())).Returns(false);

            calculator = new CalculateLifeInsuranceRiskService(
                riskScoreCalculatorBuilder,
                beingOldSpecification.Object,
                hasDependentsSpecification.Object,
                beingMarriedSpecification.Object,
                generalRiskScoreCalculator.Object);
        }

        [Fact]
        public void CalculateRisk_Should_ClassifyProfileAsIneligible_When_ProfilePresentsAnOldPerson()
        {
            // Arrange
            PersonalProfile profile = PersonalProfileFixture.CreatePersonalProfile();
            beingOldSpecification.Setup(x => x.IsAnOldPerson(It.IsAny<PersonalProfile>())).Returns(true);

            // Act
            var risk = calculator.CalculateRisk(profile);

            // Assert
            Assert.False(risk.Eligible);
        }

        [Fact]
        public void CalculateRisk_Should_GiveTheRightRisk_When_ThePersonHasDependents()
        {
            // Arrange
            PersonalProfile profile = PersonalProfileFixture.CreatePersonalProfile();
            hasDependentsSpecification.Setup(x => x.HasDependents(It.IsAny<PersonalProfile>())).Returns(true);

            // Act
            var risk = calculator.CalculateRisk(profile);

            // Assert
            Assert.Equal(1, risk.Score);
        }

        [Fact]
        public void CalculateRisk_Should_GiveTheRightRisk_When_ThePersonIsMarried()
        {
            // Arrange
            PersonalProfile profile = PersonalProfileFixture.CreatePersonalProfile();
            beingMarriedSpecification.Setup(x => x.IsMarried(It.IsAny<PersonalProfile>())).Returns(true);

            // Act
            var risk = calculator.CalculateRisk(profile);

            // Assert
            Assert.Equal(1, risk.Score);
        }

        [Fact]
        public void Should_ConsiderTheRiskOFGeneralRiskCalculators()
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
            var calculator = new CalculateLifeInsuranceRiskService(
                riskScoreCalculatorBuilder.Object,
                beingOldSpecification.Object,
                hasDependentsSpecification.Object,
                beingMarriedSpecification.Object,
                generalRiskScoreCalculator.Object);

            // Assert
            riskScoreCalculatorBuilder.Verify(p => p.AddCalculator(It.IsAny<IGeneralRiskScoreCalculator<PersonalProfile>>()), Times.Once);
        }
    }
}
