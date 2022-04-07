using RiskProfile.Domain.Models.PersonalProfileInformation;
using RiskProfile.Domain.Specifications;
using RiskProfile.UnitTests.Fixtures;
using Xunit;

namespace RiskProfile.UnitTests.Domain.Specifications
{
    public class BeingOldSpecificationTest
    {
        private readonly BeingOldSpecification specification;

        public BeingOldSpecificationTest()
        {
            specification = new();
        }

        [Theory]
        [InlineData(60, false)]
        [InlineData(61, true)]
        [InlineData(90, true)]
        [InlineData(59, false)]
        [InlineData(30, false)]
        public void IsAnOldPerson_Should_ClassifyProfileAsOldCorrectly(int age, bool expectedClassification)
        {
            // Arrange
            PersonalProfile profile = PersonalProfileFixture.CreatePersonalProfile();
            profile.Age = age;

            // Act
            var classificationResult = specification.IsAnOldPerson(profile);

            // Assert
            Assert.Equal(expectedClassification, classificationResult);

        }
    }
}
