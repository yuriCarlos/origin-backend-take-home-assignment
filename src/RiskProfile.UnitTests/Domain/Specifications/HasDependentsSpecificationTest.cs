using RiskProfile.Domain.Models.PersonalProfileInformation;
using RiskProfile.Domain.Specifications;
using RiskProfile.UnitTests.Fixtures;
using Xunit;

namespace RiskProfile.UnitTests.Domain.Specifications
{
    public class HasDependentsSpecificationTest
    {
        private readonly HasDependentsSpecification specification;

        public HasDependentsSpecificationTest()
        {
            specification = new();
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        public void HasDependents_Should_ClassifyProfileAsHavingDependentsCorrectly(int dependents, bool expectedClassification)
        {
            // Arrange
            PersonalProfile profile = PersonalProfileFixture.CreatePersonalProfile();
            profile.Dependents = dependents;

            // Act
            var classificationResult = specification.HasDependents(profile);

            // Assert
            Assert.Equal(expectedClassification, classificationResult);

        }
    }
}
