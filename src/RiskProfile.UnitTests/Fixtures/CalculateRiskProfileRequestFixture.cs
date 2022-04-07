using AutoBogus;
using Bogus;
using RiskProfileCalculator.Application.UseCases.CalculateRiskProfile;

namespace RiskProfile.UnitTests.Fixtures
{
    internal class CalculateRiskProfileRequestFixture
    {
        public static Faker<CalculateRiskProfileRequest> CreateRequest()
        {
            var possibleMaritalStatus = new[] { "single", "married" };
            var possibleOwnershipStatus = new[] { "mortgaged", "owned" };

            return new AutoFaker<CalculateRiskProfileRequest>()
                .RuleFor(p => p.Income, p => p.Random.Number(0, 1000000))
                .RuleFor(p => p.Dependents, p => p.Random.Number(0, 10))
                .RuleFor(p => p.Age, p => p.Random.Number(1, 110))
                .RuleFor(p => p.MaritalStatus, p => possibleMaritalStatus[p.Random.Number(0, 1)])
                .RuleFor(p => p.RiskQuestions, p => new int[] {
                    p.Random.Number(0, 1),
                    p.Random.Number(0, 1),
                    p.Random.Number(0, 1) })
                .RuleFor(p => p.House, p => new House { OwnershipStatus = possibleOwnershipStatus[p.Random.Number(0, 1)] })
                .RuleFor(p => p.Vehicle, p => new Vehicle { Year = p.Random.Int(0, 3000)});
        }
    }
}
