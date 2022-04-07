using RiskProfile.Domain.Interfaces.Specifications;
using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.Specifications
{
    public class MortgagedHouseSpecification : IMortgagedHouseSpecification
    {
        public bool IsMortgaged(House house) => 
            house != null 
            && house.OwnershipStatus == HouseOwnershipStatus.Mortgaged;

    }
}
