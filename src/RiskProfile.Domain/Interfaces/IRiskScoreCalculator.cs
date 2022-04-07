using RiskProfile.Domain.Models;
using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.Interfaces
{
    public interface IRiskScoreCalculator<T>
    {
        RiskScore Calculate(T obj);
    }
}
