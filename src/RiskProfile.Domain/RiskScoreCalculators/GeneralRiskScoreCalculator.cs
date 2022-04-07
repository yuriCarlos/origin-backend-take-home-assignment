using RiskProfile.Domain.Interfaces;
using RiskProfile.Domain.Interfaces.RiskScoreCalculators;
using RiskProfile.Domain.Interfaces.Specifications;
using RiskProfile.Domain.Models;
using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.RiskScoreCalculators
{
    public class GeneralRiskScoreCalculator : IGeneralRiskScoreCalculator<PersonalProfile>
    {
        private readonly IRiskScoreCalculator<PersonalProfile> _calculator;

        public GeneralRiskScoreCalculator(
            IRiskScoreCalculatorBuilder<PersonalProfile> builder,
            IAgeRiskScoreCalculator<PersonalProfile> ageRiskScoreCalculator,
            IBaseRiskScoreCalculator<PersonalProfile> baseRiskScoreCalculator,
            IHighIncomeSpecification highIncomeSpecification)
        {
            _calculator = builder
                .StartBuilding()
                .AddCalculator(ageRiskScoreCalculator)
                .AddCalculator(baseRiskScoreCalculator)
                .AddCalculator(highIncomeSpecification.HasHighIncome, new RiskScore(-1))
                .Build();
        }

        public RiskScore Calculate(PersonalProfile personalProfile) => _calculator.Calculate(personalProfile);
    }
}
