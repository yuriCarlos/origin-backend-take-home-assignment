using RiskProfile.Domain.Models;
using Xunit;

namespace RiskProfile.UnitTests.Domain.Models
{
    public class RiskScoreTest
    {
        [Theory]
        [InlineData(-1, "economic")]
        [InlineData(0, "economic")]
        [InlineData(1, "regular")]
        [InlineData(2, "regular")]
        [InlineData(3, "responsible")]
        [InlineData(4, "responsible")]
        public void Description_Should_GiveTheProperDescription_To_EachScoreValue(int scoreValue, string expectedDescription)
        {
            // Act
            var risk = new RiskScore(scoreValue);

            // Assert
            Assert.Equal(expectedDescription, risk.ScoreDescription);
        }
    }
}
