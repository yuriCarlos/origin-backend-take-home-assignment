namespace RiskProfile.Domain.Models.PersonalProfileInformation
{
    public class Vehicle
    {
        public int ManufacturedYear { get; set; }

        public Vehicle(int manufacturedYear)
        {
            ManufacturedYear = manufacturedYear;
        }
    }
}
