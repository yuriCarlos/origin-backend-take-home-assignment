using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.Interfaces.Specifications
{
    public interface IHighIncomeSpecification
    {
        bool HasHighIncome(PersonalProfile profile);
    }
}
