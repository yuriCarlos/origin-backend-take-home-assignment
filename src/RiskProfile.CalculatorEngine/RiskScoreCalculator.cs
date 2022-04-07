using RiskProfile.Domain.Interfaces;

namespace RiskProfile.Domain.Models.RiskScoreCalculators
{
    public class RiskScoreCalculator<T> : IRiskScoreCalculator<T>
    {
        public IList<IRiskScoreCalculator<T>> Calculators { get; set; }

        public RiskScoreCalculator()
        {
            Calculators = new List<IRiskScoreCalculator<T>>();
        }

        public RiskScoreCalculator(List<IRiskScoreCalculator<T>> calculators)
        {
            Calculators = calculators;
        }

        public RiskScore Calculate(T personalProfile)
        {
            var score = new RiskScore();

            foreach (var calculator in Calculators)
            {
                if (!score.Eligible)
                {
                    break;
                }

                score = SumScores(score, calculator.Calculate(personalProfile));
            }

            return score;
        }

        private RiskScore SumScores(RiskScore score1, RiskScore score2)
        {
            score1.Eligible &= score2.Eligible;
            score1.Score += score2.Score;

            return score1;
        }
    }
}
