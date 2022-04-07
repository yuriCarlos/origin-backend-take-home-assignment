using RiskProfile.Domain.Models.PersonalProfileInformation;
using RiskProfile.Domain.Specifications;
using RiskProfile.UnitTests.Fixtures;
using Xunit;

namespace RiskProfile.UnitTests.Domain.Specifications
{
    public class MortgagedHouseSpecificationTest
    {
        private readonly MortgagedHouseSpecification specification;

        public MortgagedHouseSpecificationTest()
        {
            specification = new();
        }

        [Theory]
        [InlineData(HouseOwnershipStatus.Owned, false)]
        [InlineData(HouseOwnershipStatus.Mortgaged, true)]
        public void HasMortgagedHouse_Should_ClassifyHouseCorrectly(HouseOwnershipStatus status, bool expectedClassification)
        {
            // Arrange
            var house = new House(status);

            // Act
            var classificationResult = specification.IsMortgaged(house);

            // Assert
            Assert.Equal(expectedClassification, classificationResult);
        }
    }
}
