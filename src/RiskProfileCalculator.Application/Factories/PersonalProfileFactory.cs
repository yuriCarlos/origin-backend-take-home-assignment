using RiskProfile.Domain.Models.PersonalProfileInformation;
using RiskProfileCalculator.Application.Interfaces.Factories;
using RiskProfileCalculator.Application.UseCases.CalculateRiskProfile;

namespace RiskProfileCalculator.Application.Factories
{
    public class PersonalProfileFactory : IPersonalProfileFactory
    {
        public PersonalProfile Create(CalculateRiskProfileRequest request)
        {
            var maritalStatus = (MaritalStatus)Enum.Parse(typeof(MaritalStatus), request.MaritalStatus, true);

            return new PersonalProfile(
                request.Age.Value,
                request.Dependents.Value,
                CreateHouse(request.House),
                request.Income.Value,
                maritalStatus,
                request.RiskQuestions,
                CreateVehicle(request.Vehicle));

        }

        private RiskProfile.Domain.Models.PersonalProfileInformation.House CreateHouse(UseCases.CalculateRiskProfile.House house)
        {
            if (house == null || string.IsNullOrEmpty(house.OwnershipStatus))
            {
                return null;
            }

            var ownershipStatus = (HouseOwnershipStatus)Enum.Parse(typeof(HouseOwnershipStatus), house.OwnershipStatus, true);
            return new(ownershipStatus);
        }

        private RiskProfile.Domain.Models.PersonalProfileInformation.Vehicle CreateVehicle(UseCases.CalculateRiskProfile.Vehicle vehicle)
        {
            if (vehicle == null || vehicle.Year == null)
            {
                return null;
            }

            return new(vehicle.Year.Value);
        }
    }
}
