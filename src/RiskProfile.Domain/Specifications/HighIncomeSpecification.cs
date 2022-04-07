using RiskProfile.Domain.Interfaces.Specifications;
using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.Specifications
{
    public class HighIncomeSpecification : IHighIncomeSpecification
    {
        public bool HasHighIncome(PersonalProfile profile) => profile.Income > 200000;

    }
}
