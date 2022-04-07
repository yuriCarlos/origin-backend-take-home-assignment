using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.Interfaces.Specifications
{
    public interface IHasDependentsSpecification
    {
        bool HasDependents(PersonalProfile personalProfile);
    }
}
