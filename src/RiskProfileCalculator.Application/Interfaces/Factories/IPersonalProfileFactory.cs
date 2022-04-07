using RiskProfile.Domain.Models.PersonalProfileInformation;
using RiskProfileCalculator.Application.UseCases.CalculateRiskProfile;

namespace RiskProfileCalculator.Application.Interfaces.Factories
{
    public interface IPersonalProfileFactory
    {
        PersonalProfile Create(CalculateRiskProfileRequest request);
    }
}
