using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.Interfaces.Specifications
{
    public interface IBeingOldSpecification
    {
        bool IsAnOldPerson(PersonalProfile profile);
    }
}
