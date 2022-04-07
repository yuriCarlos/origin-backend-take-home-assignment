namespace RiskProfileCalculator.Application.UseCases.CalculateRiskProfile
{
    public class CalculateRiskProfileResponse
    {
        public string Auto { get; set; }
        public string Disability { get; set; }
        public string Home { get; set; }
        public string Life { get; set; }

        public CalculateRiskProfileResponse(string auto, string disability, string home, string life)
        {
            Auto = auto;
            Disability = disability;
            Home = home;
            Life = life;
        }
    }
}
