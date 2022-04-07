using RiskProfile.Domain.Interfaces.Specifications;
using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.Specifications
{
    public class BeingOldSpecification : IBeingOldSpecification
    {
        public bool IsAnOldPerson(PersonalProfile profile) => profile.Age > 60;
    }
}
