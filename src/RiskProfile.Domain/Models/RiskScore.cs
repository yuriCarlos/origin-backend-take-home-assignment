namespace RiskProfile.Domain.Models
{
    public class RiskScore
    {
        public int Score { get; set; }
        public bool Eligible { get; set; }
        public string ScoreDescription
        {
            get
            {
                if (!Eligible)
                    return "ineligible";

                if (Score <= 0)
                    return "economic";

                if (Score >= 1 && Score <= 2)
                    return "regular";

                return "responsible";
            }
        }

        public RiskScore()
        {
            Eligible = true;
        }

        public RiskScore(bool eligible)
        {
            Eligible = eligible;
        }

        public RiskScore(int score): this()
        {
            Score = score;
        }

        public static RiskScore CreateIneligibleRiskScore()
        {
            return new RiskScore(false);
        }
    }
}
