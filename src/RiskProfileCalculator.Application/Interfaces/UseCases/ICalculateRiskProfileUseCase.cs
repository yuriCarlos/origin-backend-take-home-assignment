using RiskProfileCalculator.Application.UseCases.CalculateRiskProfile;

namespace RiskProfileCalculator.Application.Interfaces.UseCases
{
    public interface ICalculateRiskProfileUseCase
    {
        void CalculateRiskProfile(ICalculateRiskProfileUseCaseOutputPort outputPort, CalculateRiskProfileRequest request);
    }
}
