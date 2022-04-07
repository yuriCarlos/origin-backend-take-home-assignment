using RiskProfile.Domain.Interfaces;
using RiskProfile.Domain.Interfaces.RiskScoreCalculators;
using RiskProfile.Domain.Models;
using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.RiskScoreCalculators
{
    public class AgeRiskScoreCalculator : IAgeRiskScoreCalculator<PersonalProfile>
    {
        private readonly IRiskScoreCalculatorBuilder<PersonalProfile> _riskScoreCalculatorBuilder;

        public AgeRiskScoreCalculator(IRiskScoreCalculatorBuilder<PersonalProfile> riskScoreCalculatorBuilder)
        {
            _riskScoreCalculatorBuilder = riskScoreCalculatorBuilder;
        }

        public RiskScore Calculate(PersonalProfile personalProfile)
        {
            var calculator = _riskScoreCalculatorBuilder
                .StartBuilding()
                .AddCalculator(profile => profile.Age < 30, new RiskScore(-2))
                .AddCalculator(profile => profile.Age >= 30 && profile.Age <= 40, new RiskScore(-1))
                .Build();

            return calculator.Calculate(personalProfile);
        }
    }
}
