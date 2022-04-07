using RiskProfile.Domain.Interfaces;
using RiskProfile.Domain.Models;
using RiskProfile.Domain.Models.RiskScoreCalculators;

namespace RiskProfile.CalculatorEngine
{
    public class RiskScoreCalculatorBuilder<T> : IRiskScoreCalculatorBuilder<T>
    {
        public List<IRiskScoreCalculator<T>> Calculators { get; set; }

        public RiskScoreCalculatorBuilder()
        {
            Calculators = new();
        }

        public IRiskScoreCalculatorBuilder<T> AddCalculator(IRiskScoreCalculator<T> calculator)
        {
            Calculators.Add(calculator);

            return this;
        }

        public IRiskScoreCalculatorBuilder<T> AddCalculator(Func<T, bool> predicate, RiskScore addedRisk)
        {
            Calculators.Add(new PredicateCalculator<T>(predicate, addedRisk));

            return this;
        }

        public IRiskScoreCalculator<T> Build()
        {
            return new RiskScoreCalculator<T>(Calculators);
        }

        public IRiskScoreCalculatorBuilder<T> StartBuilding()
        {
            return new RiskScoreCalculatorBuilder<T>();
        }
    }
}
