using RiskProfile.Domain.Models;

namespace RiskProfile.Domain.Interfaces
{
    public interface IRiskScoreCalculatorBuilder<T>
    {
        IRiskScoreCalculatorBuilder<T> AddCalculator(Func<T, bool> predicate, RiskScore addedRisk);
        IRiskScoreCalculatorBuilder<T> AddCalculator(IRiskScoreCalculator<T> calculator);
        IRiskScoreCalculator<T> Build();
        IRiskScoreCalculatorBuilder<T> StartBuilding();
    }
}
