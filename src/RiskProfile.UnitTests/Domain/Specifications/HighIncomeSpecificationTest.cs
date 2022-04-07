using RiskProfile.Domain.Models.PersonalProfileInformation;
using RiskProfile.Domain.Specifications;
using RiskProfile.UnitTests.Fixtures;
using Xunit;

namespace RiskProfile.UnitTests.Domain.Specifications
{
    public class HighIncomeSpecificationTest
    {
        private readonly HighIncomeSpecification specification;

        public HighIncomeSpecificationTest()
        {
            specification = new();
        }

        [Theory]
        [InlineData(200000.01, true)]
        [InlineData(200000, false)]
        [InlineData(2, false)]
        [InlineData(1000000, true)]
        public void HasHighIncome_Should_ClassifyProfileAsHavingHighIncomeCorrectly(decimal income, bool expectedClassification)
        {
            // Arrange
            PersonalProfile profile = PersonalProfileFixture.CreatePersonalProfile();
            profile.Income = income;

            // Act
            var classificationResult = specification.HasHighIncome(profile);

            // Assert
            Assert.Equal(expectedClassification, classificationResult);
        }
    }
}
