using RiskProfileCalculator.Application.RequestValidators;

namespace RiskProfileCalculator.Application.UseCases.CalculateRiskProfile
{
    public interface ICalculateRiskProfileUseCaseOutputPort
    {
        public void NotifyValidationErrors(IEnumerable<Error> errors);
        public void Success(CalculateRiskProfileResponse response);
    }
}
