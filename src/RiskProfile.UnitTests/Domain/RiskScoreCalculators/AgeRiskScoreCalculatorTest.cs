using Moq;
using RiskProfile.CalculatorEngine;
using RiskProfile.Domain.Interfaces;
using RiskProfile.Domain.Models.PersonalProfileInformation;
using RiskProfile.Domain.RiskScoreCalculators;
using RiskProfile.UnitTests.Fixtures;
using Xunit;

namespace RiskProfile.UnitTests.Domain.RiskScoreCalculators
{
    public class AgeRiskScoreCalculatorTest
    {
        private readonly IRiskScoreCalculatorBuilder<PersonalProfile> riskScoreCalculatorBuilder;

        private readonly AgeRiskScoreCalculator calculator;

        public AgeRiskScoreCalculatorTest()
        {
            riskScoreCalculatorBuilder = new RiskScoreCalculatorBuilder<PersonalProfile>();

            calculator = new(riskScoreCalculatorBuilder);
        }

        [Theory]
        [InlineData(18, -2)]
        [InlineData(29, -2)]
        [InlineData(30, -1)]
        [InlineData(31, -1)]
        [InlineData(40, -1)]
        [InlineData(41, 0)]
        [InlineData(60, 0)]
        public void Calculate_Should_GiveTheRightAmountOfRiskBasedInAgePointedByProfile(int age, int expectedScoreRisk)
        {
            // Arrange
            PersonalProfile profile = PersonalProfileFixture.CreatePersonalProfile();
            profile.Age = age;

            // Act
            var risk = calculator.Calculate(profile);

            // Assert
            Assert.Equal(expectedScoreRisk, risk.Score);
        }
    }
}
