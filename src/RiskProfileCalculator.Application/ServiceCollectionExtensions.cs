using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RiskProfile.CalculatorEngine;
using RiskProfile.Domain;
using RiskProfileCalculator.Application.Factories;
using RiskProfileCalculator.Application.Interfaces.Factories;
using RiskProfileCalculator.Application.Interfaces.UseCases;
using RiskProfileCalculator.Application.RequestValidators;
using RiskProfileCalculator.Application.UseCases.CalculateRiskProfile;

namespace RiskProfileCalculator.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services
                .AddValidators()
                .AddFactories()
                .AddUseCases()
                .AddDomainServices()
                .AddCalculatorEngine();
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
            => services
            .AddScoped<IValidator<CalculateRiskProfileRequest>, CalculateRiskProfileRequestValidator>();

        private static IServiceCollection AddFactories(this IServiceCollection services)
            => services
            .AddScoped<IPersonalProfileFactory, PersonalProfileFactory>();

        private static IServiceCollection AddUseCases(this IServiceCollection services)
            => services
            .AddScoped<ICalculateRiskProfileUseCase, CalculateRiskProfileUseCase>();
    }
}
