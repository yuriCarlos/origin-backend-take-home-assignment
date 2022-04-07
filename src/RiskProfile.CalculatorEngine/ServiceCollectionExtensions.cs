using Microsoft.Extensions.DependencyInjection;
using RiskProfile.Domain.Interfaces;
using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.CalculatorEngine
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCalculatorEngine(this IServiceCollection services)
            => services
            .AddScoped<IRiskScoreCalculatorBuilder<PersonalProfile>, RiskScoreCalculatorBuilder<PersonalProfile>>();
    }
}
