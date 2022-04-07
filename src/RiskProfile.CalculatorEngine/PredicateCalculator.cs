using RiskProfile.Domain.Interfaces;
using RiskProfile.Domain.Models;
using RiskProfile.Domain.Models.PersonalProfileInformation;

namespace RiskProfile.CalculatorEngine
{
    public class PredicateCalculator<T> : IRiskScoreCalculator<T>
    {
        public Func<T, bool> Predicate { get; set; }
        public RiskScore AddedRisk { get; set; }

        public PredicateCalculator(Func<T, bool> predicate, RiskScore addedRisk)
        {
            Predicate = predicate;
            AddedRisk = addedRisk;
        }

        public RiskScore Calculate(T obj)
        {
            if (Predicate(obj))
                return AddedRisk;

            return new();
        }
    }
}
