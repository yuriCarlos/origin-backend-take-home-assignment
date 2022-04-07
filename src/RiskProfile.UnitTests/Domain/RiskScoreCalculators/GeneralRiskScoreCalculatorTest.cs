using Moq;
using RiskProfile.CalculatorEngine;
using RiskProfile.Domain.Interfaces;
using RiskProfile.Domain.Interfaces.RiskScoreCalculators;
using RiskProfile.Domain.Interfaces.Specifications;
using RiskProfile.Domain.Models;
using RiskProfile.Domain.Models.PersonalProfileInformation;
using RiskProfile.Domain.RiskScoreCalculators;
using RiskProfile.UnitTests.Fixtures;
using System;
using Xunit;

namespace RiskProfile.UnitTests.Domain.RiskScoreCalculators
{
    public class GeneralRiskScoreCalculatorTest
    {
        private readonly Mock<IRiskScoreCalculatorBuilder<PersonalProfile>> builder;
        private readonly Mock<IAgeRiskScoreCalculator<PersonalProfile>> ageRiskScoreCalculator;
        private readonly Mock<IBaseRiskScoreCalculator<PersonalProfile>> baseRiskScoreCalculator;
        private readonly Mock<IHighIncomeSpecification> highIncomeSpecification;
        private readonly GeneralRiskScoreCalculator calculator;

        public GeneralRiskScoreCalculatorTest()
        {
            builder = new();
            builder
                .Setup(p => p.StartBuilding())
                .Returns(builder.Object);

            builder
                .Setup(p => p.AddCalculator(It.IsAny<IRiskScoreCalculator<PersonalProfile>>()))
                .Returns(builder.Object);

            builder
                .Setup(p => p.AddCalculator(It.IsAny<Func<PersonalProfile, bool>>(), It.IsAny<RiskScore>()))
                .Returns(builder.Object);

            ageRiskScoreCalculator = new();
            ageRiskScoreCalculator.Setup(p => p.Calculate(It.IsAny<PersonalProfile>())).Returns(new RiskScore());

            baseRiskScoreCalculator = new();
            baseRiskScoreCalculator.Setup(p => p.Calculate(It.IsAny<PersonalProfile>())).Returns(new RiskScore());

            highIncomeSpecification = new();

            calculator = new(
                builder.Object,
                ageRiskScoreCalculator.Object,
                baseRiskScoreCalculator.Object,
                highIncomeSpecification.Object);
        }


        [Fact]
        public void Calculator_Should_AddToItselfAgeRiskScoreCalculator()
        {
            // Assert
            builder.Verify(p => p.AddCalculator(It.IsAny<IAgeRiskScoreCalculator<PersonalProfile>>()));
        }

        [Fact]
        public void Calculator_Should_AddToItselfBaseRiskScoreCalculator()
        {
            // Assert
            builder.Verify(p => p.AddCalculator(It.IsAny<IBaseRiskScoreCalculator<PersonalProfile>>()));
        }

        [Fact]
        public void Calculate_Should_GiveTheRightRisk_When_ProfileHasAHighIncome()
        {
            // Arrange
            PersonalProfile profile = PersonalProfileFixture.CreatePersonalProfile();

            var builder = new RiskScoreCalculatorBuilder<PersonalProfile>();
            highIncomeSpecification.Setup(p => p.HasHighIncome(profile)).Returns(true);

            var calculator = new GeneralRiskScoreCalculator(
                builder,
                ageRiskScoreCalculator.Object,
                baseRiskScoreCalculator.Object,
                highIncomeSpecification.Object);

            // Act
            var risk = calculator.Calculate(profile);

            // Assert
            Assert.Equal(-1, risk.Score);
        }

    }
}
