using Moq;
using RiskProfile.Domain.Interfaces;
using RiskProfile.Domain.Models;
using RiskProfile.Domain.Models.PersonalProfileInformation;
using RiskProfile.Domain.Models.RiskScoreCalculators;
using RiskProfile.UnitTests.Fixtures;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RiskProfile.UnitTests.CalculatorEngine
{
    public class RiskScoreCalculatorTest
    {
        [Fact]
        public void Calculate_Should_SumScoresCorrectly()
        {
            // Arrange
            var calculator1 = new Mock<IRiskScoreCalculator<PersonalProfile>>();
            var riskScore1 = new RiskScore(2);
            calculator1.Setup(p => p.Calculate(It.IsAny<PersonalProfile>())).Returns(riskScore1);

            var calculator2 = new Mock<IRiskScoreCalculator<PersonalProfile>>();
            var riskScore2 = new RiskScore(2);
            calculator2.Setup(p => p.Calculate(It.IsAny<PersonalProfile>())).Returns(riskScore2);

            var calculator = new RiskScoreCalculator<PersonalProfile>(new() { calculator1.Object, calculator2.Object });

            // Act
            var risk = calculator.Calculate(PersonalProfileFixture.CreatePersonalProfile());

            // Assert
            Assert.Equal(riskScore1.Score + riskScore2.Score, risk.Score);
        }

        [Fact]
        public void Calculate_Should_ReturnIneligibleScore_When_SomeOfTheScoresAreIneligible()
        {
            // Arrange
            var calculator1 = new Mock<IRiskScoreCalculator<PersonalProfile>>();
            var riskScore1 = RiskScore.CreateIneligibleRiskScore();
            calculator1.Setup(p => p.Calculate(It.IsAny<PersonalProfile>())).Returns(riskScore1);

            var calculator2 = new Mock<IRiskScoreCalculator<PersonalProfile>>();
            var riskScore2 = new RiskScore(2);
            calculator2.Setup(p => p.Calculate(It.IsAny<PersonalProfile>())).Returns(riskScore2);

            var calculator = new RiskScoreCalculator<PersonalProfile>(new() { calculator1.Object, calculator2.Object });

            // Act
            var risk = calculator.Calculate(PersonalProfileFixture.CreatePersonalProfile());

            // Assert
            Assert.False(risk.Eligible);
        }
    }
}
