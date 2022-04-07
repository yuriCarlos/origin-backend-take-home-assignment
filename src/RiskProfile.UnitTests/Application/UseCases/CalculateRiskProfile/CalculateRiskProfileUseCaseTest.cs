using FluentValidation;
using FluentValidation.Results;
using Moq;
using RiskProfile.Domain.Interfaces.Services;
using RiskProfile.Domain.Models;
using RiskProfile.Domain.Models.PersonalProfileInformation;
using RiskProfile.UnitTests.Fixtures;
using RiskProfileCalculator.Application.Interfaces.Factories;
using RiskProfileCalculator.Application.RequestValidators;
using RiskProfileCalculator.Application.UseCases.CalculateRiskProfile;
using System.Collections.Generic;
using Xunit;

namespace RiskProfile.UnitTests.Application.UseCases.CalculateRiskProfile
{
    public class CalculateRiskProfileUseCaseTest
    {
        private readonly Mock<ICalculateAutoInsuranceRiskService> calculateAutoInsuranceRiskService;
        private readonly Mock<ICalculateDisabilityInsuranceRiskService> calculateDisabilityInsuranceRiskService;
        private readonly Mock<ICalculateHomeInsuranceRiskService> calculateHomeInsuranceRiskService;
        private readonly Mock<ICalculateLifeInsuranceRiskService> calculateLifeInsuranceRiskService;
        private readonly Mock<IValidator<CalculateRiskProfileRequest>> requestValidator;
        private readonly Mock<IPersonalProfileFactory> personalProfileFactory;

        private readonly CalculateRiskProfileUseCase useCase;

        public CalculateRiskProfileUseCaseTest()
        {
            calculateAutoInsuranceRiskService = new();
            calculateAutoInsuranceRiskService
                .Setup(p => p.CalculateRisk(It.IsAny<PersonalProfile>()))
                .Returns(() => new());

            calculateDisabilityInsuranceRiskService = new();
            calculateDisabilityInsuranceRiskService
                .Setup(p => p.CalculateRisk(It.IsAny<PersonalProfile>()))
                .Returns(() => new());

            calculateHomeInsuranceRiskService = new();
            calculateHomeInsuranceRiskService
                .Setup(p => p.CalculateRisk(It.IsAny<PersonalProfile>()))
                .Returns(() => new());

            calculateLifeInsuranceRiskService = new();
            calculateLifeInsuranceRiskService
                .Setup(p => p.CalculateRisk(It.IsAny<PersonalProfile>()))
                .Returns(() => new());

            requestValidator = new();
            requestValidator
                .Setup(p => p.Validate(It.IsAny<CalculateRiskProfileRequest>()))
                .Returns(new ValidationResult(new List<ValidationFailure>()));

            personalProfileFactory = new();
            personalProfileFactory
                .Setup(p => p.Create(It.IsAny<CalculateRiskProfileRequest>()))
                .Returns(PersonalProfileFixture.CreatePersonalProfile());

            useCase = new(calculateAutoInsuranceRiskService.Object,
                calculateDisabilityInsuranceRiskService.Object,
                calculateHomeInsuranceRiskService.Object,
                calculateLifeInsuranceRiskService.Object,
                requestValidator.Object,
                personalProfileFactory.Object);
        }

        [Fact]
        public void CalculateRiskProfile_Should_ValidatesTheRequest()
        {
            // Arrange
            var request = new CalculateRiskProfileRequest();

            requestValidator
                .Setup(p => p.Validate(request))
                .Returns(new ValidationResult(new List<ValidationFailure> { new ValidationFailure("someProperty", "someError") }));

            var outputPort = new Mock<ICalculateRiskProfileUseCaseOutputPort>();

            // Act
            useCase.CalculateRiskProfile(outputPort.Object, request);

            // Assert
            outputPort.Verify(p => p.NotifyValidationErrors(It.IsAny<IEnumerable<Error>>()));
        }

        [Fact]
        public void CalculateRiskProfile_Should_ReturnTheAutoInsuranceRisk()
        {
            // Arrange
            var request = new CalculateRiskProfileRequest();

            calculateAutoInsuranceRiskService
                .Setup(p => p.CalculateRisk(It.IsAny<PersonalProfile>()))
                .Returns(RiskScore.CreateIneligibleRiskScore());

            var outputPort = new Mock<ICalculateRiskProfileUseCaseOutputPort>();
            CalculateRiskProfileResponse response = null;
            outputPort
                .Setup(p => p.Success(It.IsAny<CalculateRiskProfileResponse>()))
                .Callback<CalculateRiskProfileResponse>(p => response = p);

            // Act
            useCase.CalculateRiskProfile(outputPort.Object, request);

            // Assert
            Assert.Equal("ineligible", response.Auto);
        }

        [Fact]
        public void CalculateRiskProfile_Should_ReturnTheDisabilityInsuranceRisk()
        {
            // Arrange
            var request = new CalculateRiskProfileRequest();

            calculateDisabilityInsuranceRiskService
                .Setup(p => p.CalculateRisk(It.IsAny<PersonalProfile>()))
                .Returns(RiskScore.CreateIneligibleRiskScore());

            var outputPort = new Mock<ICalculateRiskProfileUseCaseOutputPort>();
            CalculateRiskProfileResponse response = null;
            outputPort
                .Setup(p => p.Success(It.IsAny<CalculateRiskProfileResponse>()))
                .Callback<CalculateRiskProfileResponse>(p => response = p);

            // Act
            useCase.CalculateRiskProfile(outputPort.Object, request);

            // Assert
            Assert.Equal("ineligible", response.Disability);
        }

        [Fact]
        public void CalculateRiskProfile_Should_ReturnTheLifeInsuranceRisk()
        {
            // Arrange
            var request = new CalculateRiskProfileRequest();

            calculateLifeInsuranceRiskService
                .Setup(p => p.CalculateRisk(It.IsAny<PersonalProfile>()))
                .Returns(RiskScore.CreateIneligibleRiskScore());

            var outputPort = new Mock<ICalculateRiskProfileUseCaseOutputPort>();
            CalculateRiskProfileResponse response = null;
            outputPort
                .Setup(p => p.Success(It.IsAny<CalculateRiskProfileResponse>()))
                .Callback<CalculateRiskProfileResponse>(p => response = p);

            // Act
            useCase.CalculateRiskProfile(outputPort.Object, request);

            // Assert
            Assert.Equal("ineligible", response.Life);
        }

        [Fact]
        public void CalculateRiskProfile_Should_ReturnTheHomeInsuranceRisk()
        {
            // Arrange
            var request = new CalculateRiskProfileRequest();

            calculateHomeInsuranceRiskService
                .Setup(p => p.CalculateRisk(It.IsAny<PersonalProfile>()))
                .Returns(RiskScore.CreateIneligibleRiskScore());

            var outputPort = new Mock<ICalculateRiskProfileUseCaseOutputPort>();
            CalculateRiskProfileResponse response = null;
            outputPort
                .Setup(p => p.Success(It.IsAny<CalculateRiskProfileResponse>()))
                .Callback<CalculateRiskProfileResponse>(p => response = p);

            // Act
            useCase.CalculateRiskProfile(outputPort.Object, request);

            // Assert
            Assert.Equal("ineligible", response.Home);
        }
    }
}
