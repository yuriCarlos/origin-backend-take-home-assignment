using RiskProfile.Domain.Models;
using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.Interfaces.Services
{
    public interface ICalculateLifeInsuranceRiskService
    {
        RiskScore CalculateRisk(PersonalProfile personalProfile);
    }
}
