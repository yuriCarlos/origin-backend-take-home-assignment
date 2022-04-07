using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.Interfaces.Specifications
{
    public interface INewVehicleSpecification
    {
        bool IsANewVehicle(Vehicle vehicle);
    }
}
