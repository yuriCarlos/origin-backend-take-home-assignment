using RiskProfile.Domain.Interfaces.Specifications;
using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.Specifications
{
    public class HasDependentsSpecification : IHasDependentsSpecification
    {
        public bool HasDependents(PersonalProfile personalProfile) => personalProfile.Dependents > 0;
    }
}
