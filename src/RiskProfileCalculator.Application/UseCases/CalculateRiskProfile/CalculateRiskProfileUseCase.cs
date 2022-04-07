using FluentValidation;
using RiskProfile.Domain.Interfaces.Services;
using RiskProfileCalculator.Application.Interfaces.Factories;
using RiskProfileCalculator.Application.Interfaces.UseCases;
using RiskProfileCalculator.Application.RequestValidators;

namespace RiskProfileCalculator.Application.UseCases.CalculateRiskProfile
{
    public class CalculateRiskProfileUseCase: ICalculateRiskProfileUseCase
    {
        private readonly ICalculateAutoInsuranceRiskService _calculateAutoInsuranceRiskService;
        private readonly ICalculateDisabilityInsuranceRiskService _calculateDisabilityInsuranceRiskService;
        private readonly ICalculateHomeInsuranceRiskService _calculateHomeInsuranceRiskService;
        private readonly ICalculateLifeInsuranceRiskService _calculateLifeInsuranceRiskService;
        private readonly IValidator<CalculateRiskProfileRequest> _requestValidator;
        private readonly IPersonalProfileFactory _personalProfileFactory;

        public CalculateRiskProfileUseCase(ICalculateAutoInsuranceRiskService calculateAutoInsuranceRiskService,
            ICalculateDisabilityInsuranceRiskService calculateDisabilityInsuranceRiskService,
            ICalculateHomeInsuranceRiskService calculateHomeInsuranceRiskService,
            ICalculateLifeInsuranceRiskService calculateLifeInsuranceRiskService,
            IValidator<CalculateRiskProfileRequest> requestValidator,
            IPersonalProfileFactory personalProfileFactory)
        {
            _calculateAutoInsuranceRiskService = calculateAutoInsuranceRiskService;
            _calculateDisabilityInsuranceRiskService = calculateDisabilityInsuranceRiskService;
            _calculateHomeInsuranceRiskService = calculateHomeInsuranceRiskService;
            _calculateLifeInsuranceRiskService = calculateLifeInsuranceRiskService;
            _requestValidator = requestValidator;
            _personalProfileFactory = personalProfileFactory;
        }

        public void CalculateRiskProfile(ICalculateRiskProfileUseCaseOutputPort outputPort, CalculateRiskProfileRequest request)
        {
            var validationResult = _requestValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                outputPort.NotifyValidationErrors(validationResult.Errors.Select(p => new Error(p.ErrorCode, p.ErrorMessage)));

                return;
            }

            var personalProfile = _personalProfileFactory.Create(request);

            var autoInsuranceRisk = _calculateAutoInsuranceRiskService.CalculateRisk(personalProfile);
            var lifeInsuranceRisk = _calculateLifeInsuranceRiskService.CalculateRisk(personalProfile);
            var disabilityInsuranceRisk = _calculateDisabilityInsuranceRiskService.CalculateRisk(personalProfile);
            var homeInsuranceRisk = _calculateHomeInsuranceRiskService.CalculateRisk(personalProfile);

            var response = new CalculateRiskProfileResponse(
                autoInsuranceRisk.ScoreDescription,
                disabilityInsuranceRisk.ScoreDescription,
                homeInsuranceRisk.ScoreDescription,
                lifeInsuranceRisk.ScoreDescription);

            outputPort.Success(response);
        }
    }
}
