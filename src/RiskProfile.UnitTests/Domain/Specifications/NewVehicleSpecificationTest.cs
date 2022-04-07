using RiskProfile.Domain.Models.PersonalProfileInformation;
using RiskProfile.Domain.Specifications;
using RiskProfile.UnitTests.Fixtures;
using System;
using Xunit;

namespace RiskProfile.UnitTests.Domain.Specifications
{
    public class NewVehicleSpecificationTest
    {
        private readonly NewVehicleSpecification specification;

        public NewVehicleSpecificationTest()
        {
            specification = new();
        }

        [Theory]
        [InlineData(5, true)]
        [InlineData(6, false)]
        [InlineData(7, false)]
        [InlineData(2, true)]
        public void IsANewVehicle_Should_ClassifyVehicleAsNewCorrectly(int yearsToSubtract, bool expectedClassification)
        {
            // Arrange
            var vehicle = new Vehicle(DateTime.Now.Year - yearsToSubtract);

            // Act
            var classificationResult = specification.IsANewVehicle(vehicle);

            // Assert
            Assert.Equal(expectedClassification, classificationResult);
        }
    }
}
