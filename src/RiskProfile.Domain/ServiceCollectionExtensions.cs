using Microsoft.Extensions.DependencyInjection;
using RiskProfile.Domain.Interfaces;
using RiskProfile.Domain.Interfaces.RiskScoreCalculators;
using RiskProfile.Domain.Interfaces.Services;
using RiskProfile.Domain.Interfaces.Specifications;
using RiskProfile.Domain.Models.PersonalProfileInformation;
using RiskProfile.Domain.RiskScoreCalculators;
using RiskProfile.Domain.Services;
using RiskProfile.Domain.Specifications;

namespace RiskProfile.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            return services
                .AddSpecifications()
                .AddServices()
                .AddCalculators();
        }

        public static IServiceCollection AddSpecifications(this IServiceCollection services)
            => services
            .AddScoped<IBeingMarriedSpecification, BeingMarriedSpecification>()
            .AddScoped<IBeingOldSpecification, BeingOldSpecification>()
            .AddScoped<IHasDependentsSpecification, HasDependentsSpecification>()
            .AddScoped<IHighIncomeSpecification, HighIncomeSpecification>()
            .AddScoped<IMortgagedHouseSpecification, MortgagedHouseSpecification>()
            .AddScoped<INewVehicleSpecification, NewVehicleSpecification>();

        public static IServiceCollection AddServices(this IServiceCollection services)
            => services
            .AddScoped<ICalculateAutoInsuranceRiskService, CalculateAutoInsuranceRiskService>()
            .AddScoped<ICalculateDisabilityInsuranceRiskService, CalculateDisabilityInsuranceRiskService>()
            .AddScoped<ICalculateHomeInsuranceRiskService, CalculateHomeInsuranceRiskService>()
            .AddScoped<ICalculateLifeInsuranceRiskService, CalculateLifeInsuranceRiskService>();

        public static IServiceCollection AddCalculators(this IServiceCollection services)
            => services
            .AddScoped<IAgeRiskScoreCalculator<PersonalProfile>, AgeRiskScoreCalculator>()
            .AddScoped<IBaseRiskScoreCalculator<PersonalProfile>, BaseRiskScoreCalculator>()
            .AddScoped<IGeneralRiskScoreCalculator<PersonalProfile>, GeneralRiskScoreCalculator>();
    }
}
