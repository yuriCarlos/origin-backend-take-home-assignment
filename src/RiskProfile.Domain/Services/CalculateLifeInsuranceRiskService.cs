using RiskProfile.Domain.Interfaces;
using RiskProfile.Domain.Interfaces.Services;
using RiskProfile.Domain.Interfaces.Specifications;
using RiskProfile.Domain.Models;
using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.Services
{
    public class CalculateLifeInsuranceRiskService: ICalculateLifeInsuranceRiskService
    {
        private readonly IRiskScoreCalculator<PersonalProfile> _scoreCalculator;

        public CalculateLifeInsuranceRiskService(IRiskScoreCalculatorBuilder<PersonalProfile> riskScoreCalculatorBuilder,
            IBeingOldSpecification beingOldSpecification,
            IHasDependentsSpecification hasDependentsSpecification,
            IBeingMarriedSpecification beingMarriedSpecification,
            IGeneralRiskScoreCalculator<PersonalProfile> generalRiskScoreCalculator)
        {
            _scoreCalculator = riskScoreCalculatorBuilder
                .StartBuilding()
                .AddCalculator(generalRiskScoreCalculator)
                .AddCalculator(beingOldSpecification.IsAnOldPerson, RiskScore.CreateIneligibleRiskScore())
                .AddCalculator(hasDependentsSpecification.HasDependents, new RiskScore(1))
                .AddCalculator(beingMarriedSpecification.IsMarried, new RiskScore(1))
                .Build();
        }

        public RiskScore CalculateRisk(PersonalProfile personalProfile) => _scoreCalculator.Calculate(personalProfile);
    }
}
