namespace RiskProfile.Domain.Models.PersonalProfileInformation
{
    public class House
    {
        public HouseOwnershipStatus OwnershipStatus { get; set; }

        public House(HouseOwnershipStatus ownershipStatus)
        {
            OwnershipStatus = ownershipStatus;
        }
    }
}
