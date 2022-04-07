using RiskProfile.UnitTests.Fixtures;
using RiskProfileCalculator.Application.RequestValidators;
using RiskProfileCalculator.Application.UseCases.CalculateRiskProfile;
using System.Linq;
using Xunit;

namespace RiskProfile.UnitTests.Application.RequestValidators
{
    public class CalculateRiskProfileRequestValidatorTest
    {
        private readonly CalculateRiskProfileRequestValidator validator;
        public CalculateRiskProfileRequestValidatorTest()
        {
            validator = new();
        }

        [Fact]
        public void Should_Return_AgeShouldNotBeNull_When_AgeIsNull()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.Age = null;

            // Act
            var response = validator.Validate(request);

            // Assert
            var error = response.Errors.First();
            Assert.Equal(ErrorTypes.AgeShouldNotBeNull.Code, error.ErrorCode);
        }

        [Fact]
        public void Should_Return_AgeShouldBeEqualOrGreaterThan0_When_AgeIsNull()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.Age = -1;

            // Act
            var response = validator.Validate(request);

            // Assert
            var error = response.Errors.First();
            Assert.Equal(ErrorTypes.AgeShouldBeEqualOrGreaterThan0.Code, error.ErrorCode);
        }

        [Fact]
        public void Should_MarkAsValid_When_AgeIsGreaterThan0()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.Age = 1;

            // Act
            var response = validator.Validate(request);

            // Assert
            Assert.True(response.IsValid);
        }

        [Fact]
        public void Should_ReturnDependentsShouldNotBeNull_When_DependentsIsNull()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.Dependents = null;

            // Act
            var response = validator.Validate(request);

            // Assert
            var error = response.Errors.First();
            Assert.Equal(ErrorTypes.DependentsShouldNotBeNull.Code, error.ErrorCode);
        }

        [Fact]
        public void Should_ReturnDependentsShouldBeGreaterThan0_When_DependentsIsLowerThan0()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.Dependents = -1;

            // Act
            var response = validator.Validate(request);

            // Assert
            var error = response.Errors.First();
            Assert.Equal(ErrorTypes.DependentsShouldBeEqualOrGreaterThan0.Code, error.ErrorCode);
        }

        [Fact]
        public void Should_PassValidations_When_DependentsIsGreaterThanOrEqualTo0()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.Dependents = 0;

            // Act
            var response = validator.Validate(request);

            // Assert
            Assert.True(response.IsValid);
        }

        [Fact]
        public void Should_PassValidations_When_IncomeIsGreaterOrEqualTo0()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.Income = 0;

            // Act
            var response = validator.Validate(request);

            // Assert
            Assert.True(response.IsValid);
        }

        [Fact]
        public void Should_ReturnIncomeShouldBeGreaterOrEqualTo0_When_IncomeIsLowerThan0()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.Income = -1;

            // Act
            var response = validator.Validate(request);

            // Assert
            var error = response.Errors.First();
            Assert.Equal(ErrorTypes.IncomeShouldBeEqualOrGreaterThan0.Code, error.ErrorCode);
        }

        [Fact]
        public void Should_ReturnIncomeShouldNotBeNull_When_IncomeIsNull()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.Income = null;

            // Act
            var response = validator.Validate(request);

            // Assert
            var error = response.Errors.First();
            Assert.Equal(ErrorTypes.IncomeShouldNotBeNull.Code, error.ErrorCode);
        }

        [Fact]
        public void Should_ReturnMaritalStatusShouldNotBeNull_When_MaritalStatusIsNull()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.MaritalStatus = null;

            // Act
            var response = validator.Validate(request);

            // Assert
            var error = response.Errors.First();
            Assert.Equal(ErrorTypes.MaritalStatusShouldNotBeNull.Code, error.ErrorCode);
        }

        [Fact]
        public void Should_ReturnMaritalStatusShouldBeValid_When_MaritalStatusIsInvalid()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.MaritalStatus = "something that's not martial status";

            // Act
            var response = validator.Validate(request);

            // Assert
            var error = response.Errors.First();
            Assert.Equal(ErrorTypes.MaritalStatusShouldBeValid.Code, error.ErrorCode);
        }

        [Fact]
        public void Should_PassValidation_When_MaritalStatusIsValid()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.MaritalStatus = "single";

            // Act
            var response = validator.Validate(request);

            // Assert
            Assert.True(response.IsValid);
        }

        [Fact]
        public void Should_ReturnRiskQuestionsShouldBeValid_When_RiskQuestionsAreInADifferentRangeOfThePermitedOne()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.RiskQuestions = new[] { 2, 0, 3 };

            // Act
            var response = validator.Validate(request);

            // Assert
            var error = response.Errors.First();
            Assert.Equal(ErrorTypes.RiskQuestionsShouldBeValid.Code, error.ErrorCode);
        }

        [Fact]
        public void Should_ReturnHouseOwnershipStatusMustBeValid_When_HouseOwnershipStatusIsInvalid()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.House.OwnershipStatus = "something that's not an ownership status";

            // Act
            var response = validator.Validate(request);

            // Assert
            var error = response.Errors.First();
            Assert.Equal(ErrorTypes.HouseOwnershipStatusMustBeValid.Code, error.ErrorCode);
        }

        [Fact]
        public void Should_Pass_When_ThereIsNoHouse()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.House = null;

            // Act
            var response = validator.Validate(request);

            // Assert
            Assert.True(response.IsValid);
        }

        [Fact]
        public void Should_VehicleYearShouldBeGreaterThan0_When_YearLesserThan1()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.Vehicle.Year = -1;

            // Act
            var response = validator.Validate(request);

            // Assert
            var error = response.Errors.First();
            Assert.Equal(ErrorTypes.VehicleYearShouldBeGreaterThan0.Code, error.ErrorCode);
        }

        [Fact]
        public void Should_Pass_When_ThereIsNoVehicle()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            request.Vehicle = null;

            // Act
            var response = validator.Validate(request);

            // Assert
            Assert.True(response.IsValid);
        }

    }
}
