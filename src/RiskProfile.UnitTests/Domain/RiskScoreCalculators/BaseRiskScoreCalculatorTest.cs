using RiskProfile.Domain.Models.PersonalProfileInformation;
using RiskProfile.Domain.RiskScoreCalculators;
using RiskProfile.UnitTests.Fixtures;
using System.Linq;
using Xunit;

namespace RiskProfile.UnitTests.Domain.RiskScoreCalculators
{
    public class BaseRiskScoreCalculatorTest
    {
        public readonly BaseRiskScoreCalculator calculator;
        public BaseRiskScoreCalculatorTest()
        {
            calculator = new();
        }

        [Theory]
        [InlineData(1,1,1)]
        [InlineData(0,0,1)]
        [InlineData(1,0,0)]
        [InlineData(1,0,1)]
        public void Calculate_Should_CalculateTheRightBaseRisk(int firstQuestion, int secondQuestion, int thirdQuestion)
        {
            // Arrange
            PersonalProfile profile = PersonalProfileFixture.CreatePersonalProfile();
            profile.RiskQuestions = new[] {firstQuestion, secondQuestion, thirdQuestion};
            
            // Act
            var risk = calculator.Calculate(profile);

            // Assert
            Assert.Equal(profile.RiskQuestions.Sum(), risk.Score);
        }
    }
}
