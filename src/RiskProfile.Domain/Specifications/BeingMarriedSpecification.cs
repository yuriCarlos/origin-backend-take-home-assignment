using RiskProfile.Domain.Interfaces.Specifications;
using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.Specifications
{
    public class BeingMarriedSpecification : IBeingMarriedSpecification
    {
        public bool IsMarried(PersonalProfile personalProfile) => personalProfile.MaritalStatus == MaritalStatus.Married;
    }
}
