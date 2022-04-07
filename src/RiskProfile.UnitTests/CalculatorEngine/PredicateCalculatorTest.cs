using RiskProfile.CalculatorEngine;
using RiskProfile.Domain.Models;
using RiskProfile.Domain.Models.PersonalProfileInformation;
using RiskProfile.UnitTests.Fixtures;
using Xunit;

namespace RiskProfile.UnitTests.CalculatorEngine
{
    public class PredicateCalculatorTest
    {
        [Fact]
        public void Calculate_Should_ReturnTheDefinedRisk_When_PredicateReturnsTrue()
        {
            // Arrange
            var riskScore = new RiskScore(15);
            var calculator = new PredicateCalculator<PersonalProfile>(_ => true, riskScore);

            // Act
            var risk = calculator.Calculate(PersonalProfileFixture.CreatePersonalProfile());

            // Assert
            Assert.Equal(riskScore, risk);
        }

        [Fact]
        public void Calculate_Should_ReturnABlankRiskScore_When_PredicateReturnsFalse()
        {
            // Arrange
            var riskScore = new RiskScore(15);
            var calculator = new PredicateCalculator<PersonalProfile>(_ => false, riskScore);

            // Act
            var risk = calculator.Calculate(PersonalProfileFixture.CreatePersonalProfile());

            // Assert
            Assert.True(risk.Eligible);
            Assert.Equal(0, risk.Score);
        }
    }
}
