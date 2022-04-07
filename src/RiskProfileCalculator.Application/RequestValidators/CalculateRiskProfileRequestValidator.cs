using FluentValidation;
using RiskProfileCalculator.Application.UseCases.CalculateRiskProfile;

namespace RiskProfileCalculator.Application.RequestValidators
{
    public class CalculateRiskProfileRequestValidator : AbstractValidator<CalculateRiskProfileRequest>
    {
        public CalculateRiskProfileRequestValidator()
        {
            RuleFor(p => p.Age)
                .NotNull()
                .WithErrorCode(ErrorTypes.AgeShouldNotBeNull.Code)
                .WithMessage(ErrorTypes.AgeShouldNotBeNull.Message)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode(ErrorTypes.AgeShouldBeEqualOrGreaterThan0.Code)
                .WithMessage(ErrorTypes.AgeShouldBeEqualOrGreaterThan0.Message);

            RuleFor(p => p.Dependents)
                .NotNull()
                .WithErrorCode(ErrorTypes.DependentsShouldNotBeNull.Code)
                .WithMessage(ErrorTypes.DependentsShouldNotBeNull.Message)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode(ErrorTypes.DependentsShouldBeEqualOrGreaterThan0.Code)
                .WithMessage(ErrorTypes.DependentsShouldBeEqualOrGreaterThan0.Message);

            RuleFor(p => p.Income)
                .NotNull()
                .WithErrorCode(ErrorTypes.IncomeShouldNotBeNull.Code)
                .WithMessage(ErrorTypes.IncomeShouldNotBeNull.Message)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode(ErrorTypes.IncomeShouldBeEqualOrGreaterThan0.Code)
                .WithMessage(ErrorTypes.IncomeShouldBeEqualOrGreaterThan0.Message);

            var possibleMaritalStatus = new[] { "single", "married" };
            RuleFor(p => p.MaritalStatus)
                .NotNull()
                .WithErrorCode(ErrorTypes.MaritalStatusShouldNotBeNull.Code)
                .WithMessage(ErrorTypes.MaritalStatusShouldNotBeNull.Message)
                .Must(p =>
                {
                    return p != null && possibleMaritalStatus.Contains(p.ToLower());
                })
                .WithErrorCode(ErrorTypes.MaritalStatusShouldBeValid.Code)
                .WithMessage(string.Format(ErrorTypes.MaritalStatusShouldBeValid.Message, string.Join(", ", possibleMaritalStatus)));

            RuleFor(p => p.RiskQuestions)
                .Must(riskQuestions => 
                    riskQuestions != null
                    && riskQuestions.Length == 3
                    && !riskQuestions.Any(answer => answer > 1 || answer < 0))
                .WithErrorCode(ErrorTypes.RiskQuestionsShouldBeValid.Code)
                .WithMessage(ErrorTypes.RiskQuestionsShouldBeValid.Message);

            var possibleOwnershipStatus = new[] { "owned", "mortgaged" };
            RuleFor(p => p.House)
                .Must(house =>
                    house == null
                    || string.IsNullOrEmpty(house.OwnershipStatus)
                    || possibleOwnershipStatus.Contains(house.OwnershipStatus))
                .WithErrorCode(ErrorTypes.HouseOwnershipStatusMustBeValid.Code)
                .WithMessage(string.Format(ErrorTypes.HouseOwnershipStatusMustBeValid.Message, string.Join(", ", possibleOwnershipStatus)));

            RuleFor(p => p.Vehicle)
                .Must(vehicle =>
                    vehicle == null
                    || vehicle.Year == null
                    || vehicle.Year > 0)
                .WithErrorCode(ErrorTypes.VehicleYearShouldBeGreaterThan0.Code)
                .WithMessage(ErrorTypes.VehicleYearShouldBeGreaterThan0.Message);
        }
    }
}
