using RiskProfile.Domain.Interfaces.Specifications;
using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.Specifications
{
    public class NewVehicleSpecification : INewVehicleSpecification
    {
        public bool IsANewVehicle(Vehicle vehicle) => 
            vehicle != null 
            && vehicle.ManufacturedYear >= DateTime.Now.Year - 5;
    }
}
