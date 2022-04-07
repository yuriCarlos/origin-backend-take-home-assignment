using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.Interfaces.Specifications
{
    public interface IMortgagedHouseSpecification
    {
        bool IsMortgaged(House house);
    }
}
