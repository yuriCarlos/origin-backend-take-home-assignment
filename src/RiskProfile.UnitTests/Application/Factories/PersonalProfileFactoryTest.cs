using RiskProfile.Domain.Models.PersonalProfileInformation;
using RiskProfile.UnitTests.Fixtures;
using RiskProfileCalculator.Application.Factories;
using RiskProfileCalculator.Application.UseCases.CalculateRiskProfile;
using Xunit;

namespace RiskProfile.UnitTests.Application.Factories
{
    public class PersonalProfileFactoryTest
    {
        private readonly PersonalProfileFactory factory;

        public PersonalProfileFactoryTest()
        {
            factory = new();
        }

        [Fact]
        public void Create_Should_MapPropertiesCorrectly()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();

            // Act
            var personalProfile = factory.Create(request);

            // Assert
            Assert.Equal(request.Income, personalProfile.Income);
            Assert.Equal(request.Dependents, personalProfile.Dependents);
            Assert.Equal(request.Age, personalProfile.Age);
            Assert.Equal(request.RiskQuestions, personalProfile.RiskQuestions);
        }

        [Fact]
        public void Create_Should_MapVehicleAsNull_When_ThereIsNoYearInformed()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.Vehicle.Year = null;

            // Act
            var personalProfile = factory.Create(request);

            // Assert
            Assert.Null(personalProfile.Vehicle);
        }

        [Fact]
        public void Create_Should_MapVehicleAsNull_When_ThereIsNoVehicleInformed()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.Vehicle = null;

            // Act
            var personalProfile = factory.Create(request);

            // Assert
            Assert.Null(personalProfile.Vehicle);
        }

        [Fact]
        public void Create_Should_MapVehicleCorrectly_When_ThereIsAVehicle()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();

            // Act
            var personalProfile = factory.Create(request);

            // Assert
            Assert.Equal(request.Vehicle.Year, personalProfile.Vehicle.ManufacturedYear);
        }

        [Fact]
        public void Create_Should_MapHouseAsNull_When_ThereIsNoOwnershipStatusInformed()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.House.OwnershipStatus = null;

            // Act
            var personalProfile = factory.Create(request);

            // Assert
            Assert.Null(personalProfile.House);
        }

        [Fact]
        public void Create_Should_MapHouseAsNull_When_ThereIsNoHouseInformed()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.House = null;

            // Act
            var personalProfile = factory.Create(request);

            // Assert
            Assert.Null(personalProfile.House);
        }

        [Theory]
        [InlineData("owned", HouseOwnershipStatus.Owned)]
        [InlineData("oWned", HouseOwnershipStatus.Owned)]
        [InlineData("Mortgaged", HouseOwnershipStatus.Mortgaged)]
        [InlineData("mortgaged", HouseOwnershipStatus.Mortgaged)]
        public void Create_Should_MapHouseCorrectly_When_ThereIsAHouse(string ownershipStatus, HouseOwnershipStatus expectedOwnershipStatus)
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.House.OwnershipStatus = ownershipStatus;

            // Act
            var personalProfile = factory.Create(request);

            // Assert
            Assert.Equal(expectedOwnershipStatus, personalProfile.House.OwnershipStatus);
        }

        [Theory]
        [InlineData("married", MaritalStatus.Married)]
        [InlineData("marriEd", MaritalStatus.Married)]
        [InlineData("siNgle", MaritalStatus.Single)]
        [InlineData("single", MaritalStatus.Single)]
        public void Create_Should_MapCorrectlyTheMaritalStatuses(string maritalStatus, MaritalStatus expectedMaritalStatus)
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.MaritalStatus = maritalStatus;

            // Act
            var personalProfile = factory.Create(request);

            // Assert
            Assert.Equal(expectedMaritalStatus, personalProfile.MaritalStatus);
        }
    }
}
