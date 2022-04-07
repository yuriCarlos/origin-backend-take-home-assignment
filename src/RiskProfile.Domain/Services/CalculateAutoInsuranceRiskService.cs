using RiskProfile.Domain.Interfaces;
using RiskProfile.Domain.Interfaces.Services;
using RiskProfile.Domain.Interfaces.Specifications;
using RiskProfile.Domain.Models;
using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.Services
{
    public class CalculateAutoInsuranceRiskService: ICalculateAutoInsuranceRiskService
    {
        private readonly IRiskScoreCalculator<PersonalProfile> _scoreCalculator;

        public CalculateAutoInsuranceRiskService(IRiskScoreCalculatorBuilder<PersonalProfile> riskScoreCalculatorBuilder,
            INewVehicleSpecification newVehicleSpecification,
            IGeneralRiskScoreCalculator<PersonalProfile> generalRiskScoreCalculator)
        {
            _scoreCalculator = riskScoreCalculatorBuilder
                .StartBuilding()
                .AddCalculator(generalRiskScoreCalculator)
                .AddCalculator(personalProfile => personalProfile.Vehicle == null, RiskScore.CreateIneligibleRiskScore())
                .AddCalculator(personalProfile => newVehicleSpecification.IsANewVehicle(personalProfile.Vehicle), new RiskScore(1))
                .Build();
        }

        public RiskScore CalculateRisk(PersonalProfile personalProfile) => _scoreCalculator.Calculate(personalProfile);
    }
}
