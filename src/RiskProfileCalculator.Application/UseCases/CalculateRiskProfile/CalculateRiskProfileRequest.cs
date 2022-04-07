using RiskProfile.Domain.Models.PersonalProfileInformation;
using System.Text.Json.Serialization;

namespace RiskProfileCalculator.Application.UseCases.CalculateRiskProfile
{
    public class CalculateRiskProfileRequest
    {
        public int? Age { get; set; }

        public int? Dependents { get; set; }

        public decimal? Income { get; set; }

        [JsonPropertyName("marital_status")]
        public string? MaritalStatus { get; set; }

        [JsonPropertyName("risk_questions")]
        public int[]? RiskQuestions { get; set; }

        public House? House { get; set; }

        public Vehicle? Vehicle { get; set; }
    }

    public class House
    {
        [JsonPropertyName("ownership_status")]
        public string? OwnershipStatus { get; set; }
    }

    public class Vehicle
    {
        public int? Year { get; set; }
    }
}
