using RiskProfile.Domain.Interfaces;
using RiskProfile.Domain.Interfaces.Services;
using RiskProfile.Domain.Interfaces.Specifications;
using RiskProfile.Domain.Models;
using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.Services
{
    public class CalculateDisabilityInsuranceRiskService: ICalculateDisabilityInsuranceRiskService
    {
        private readonly IRiskScoreCalculator<PersonalProfile> _scoreCalculator;

        public CalculateDisabilityInsuranceRiskService(IRiskScoreCalculatorBuilder<PersonalProfile> riskScoreCalculatorBuilder,
            IBeingOldSpecification beingOldSpecification,
            IMortgagedHouseSpecification mortgagedHouseSpecification,
            IHasDependentsSpecification hasDependentsSpecification,
            IBeingMarriedSpecification beingMarriedSpecification,
            IGeneralRiskScoreCalculator<PersonalProfile> generalRiskScoreCalculator)
        {
            _scoreCalculator = riskScoreCalculatorBuilder
                .StartBuilding()
                .AddCalculator(generalRiskScoreCalculator)
                .AddCalculator(personalProfile => personalProfile.Income <= 0, RiskScore.CreateIneligibleRiskScore())
                .AddCalculator(beingOldSpecification.IsAnOldPerson, RiskScore.CreateIneligibleRiskScore())
                .AddCalculator(personalProfile => mortgagedHouseSpecification.IsMortgaged(personalProfile.House), new RiskScore(1))
                .AddCalculator(hasDependentsSpecification.HasDependents, new RiskScore(1))
                .AddCalculator(beingMarriedSpecification.IsMarried, new RiskScore(-1))
                .Build();
        }

        public RiskScore CalculateRisk(PersonalProfile personalProfile)
        {
            return _scoreCalculator.Calculate(personalProfile);
        }
    }
}
