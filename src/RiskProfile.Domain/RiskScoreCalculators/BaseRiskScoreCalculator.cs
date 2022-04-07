using RiskProfile.Domain.Interfaces.RiskScoreCalculators;
using RiskProfile.Domain.Models;
using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.Domain.RiskScoreCalculators
{
    public class BaseRiskScoreCalculator : IBaseRiskScoreCalculator<PersonalProfile>
    {
        public RiskScore Calculate(PersonalProfile personalProfile)
        {
            var score = new RiskScore();
            score.Score = personalProfile.RiskQuestions.Sum();

            return score;
        }
    }
}
