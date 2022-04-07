using Microsoft.AspNetCore.Mvc;
using Moq;
using RiskProfile.Api.Controllers;
using RiskProfile.UnitTests.Fixtures;
using RiskProfileCalculator.Application.Interfaces.UseCases;
using RiskProfileCalculator.Application.RequestValidators;
using RiskProfileCalculator.Application.UseCases.CalculateRiskProfile;
using System.Collections.Generic;
using Xunit;

namespace RiskProfile.UnitTests.Api.Controllers
{
    public class CalculateRiskControllerTest
    {
        private readonly Mock<ICalculateRiskProfileUseCase> calculateRiskProfileUseCase;

        private readonly CalculateRiskController controller;
        public CalculateRiskControllerTest()
        {
            calculateRiskProfileUseCase = new();
            controller = new(calculateRiskProfileUseCase.Object);
        }

        [Fact]
        public void Calculate_Should_Return400_When_TheRequestIsInvalid()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();
            controller.NotifyValidationErrors(new List<Error> { new Error("some", "error") });

            // Act
            var response = controller.Calculate(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public void Calculate_Should_Return200_When_TheRequestIsInvalid()
        {
            // Arrange
            CalculateRiskProfileRequest request = CalculateRiskProfileRequestFixture.CreateRequest();

            // Act
            var response = controller.Calculate(request);

            // Assert
            Assert.IsType<OkObjectResult>(response);
        }
    }
}
