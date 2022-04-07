using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.Interfaces.Specifications
{
    public interface IBeingMarriedSpecification
    {
        bool IsMarried(PersonalProfile personalProfile);
    }
}
