using RiskProfile.Domain.Interfaces;
using RiskProfile.Domain.Interfaces.Services;
using RiskProfile.Domain.Interfaces.Specifications;
using RiskProfile.Domain.Models;
using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.Services
{
    public class CalculateHomeInsuranceRiskService: ICalculateHomeInsuranceRiskService
    {
        private readonly IRiskScoreCalculator<PersonalProfile> _scoreCalculator;

        public CalculateHomeInsuranceRiskService(IRiskScoreCalculatorBuilder<PersonalProfile> riskScoreCalculatorBuilder,
            IMortgagedHouseSpecification mortgagedHouseSpecification,
            IGeneralRiskScoreCalculator<PersonalProfile> generalRiskScoreCalculator)
        {
            _scoreCalculator = riskScoreCalculatorBuilder
                .StartBuilding()
                .AddCalculator(generalRiskScoreCalculator)
                .AddCalculator(personalProfile => personalProfile.House == null, RiskScore.CreateIneligibleRiskScore())
                .AddCalculator(personalProfile => mortgagedHouseSpecification.IsMortgaged(personalProfile.House), new RiskScore(1))
                .Build();
        }

        public RiskScore CalculateRisk(PersonalProfile personalProfile) => _scoreCalculator.Calculate(personalProfile);
    }
}
