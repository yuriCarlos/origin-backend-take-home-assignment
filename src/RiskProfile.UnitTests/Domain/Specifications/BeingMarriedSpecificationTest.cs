using RiskProfile.Domain.Models.PersonalProfileInformation;
using RiskProfile.Domain.Specifications;
using RiskProfile.UnitTests.Fixtures;
using Xunit;

namespace RiskProfile.UnitTests.Domain.Specifications
{
    public class BeingMarriedSpecificationTest
    {
        private readonly BeingMarriedSpecification specification;

        public BeingMarriedSpecificationTest()
        {
            specification = new();
        }

        [Theory]
        [InlineData(MaritalStatus.Married, true)]
        [InlineData(MaritalStatus.Single, false)]
        public void IsMarried_Should_ClassifyProfileAsMarriedCorrectly(MaritalStatus maritalStatus, bool expectedClassification)
        {
            // Arrange
            PersonalProfile profile = PersonalProfileFixture.CreatePersonalProfile();
            profile.MaritalStatus = maritalStatus;

            // Act
            var classificationResult = specification.IsMarried(profile);

            // Assert
            Assert.Equal(expectedClassification, classificationResult);

        }
    }
}
