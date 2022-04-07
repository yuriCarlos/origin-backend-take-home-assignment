using Moq;
using RiskProfile.CalculatorEngine;
using RiskProfile.Domain.Interfaces;
using RiskProfile.Domain.Models;
using RiskProfile.Domain.Models.PersonalProfileInformation;
using RiskProfile.Domain.Models.RiskScoreCalculators;
using System.Linq;
using Xunit;

namespace RiskProfile.UnitTests.CalculatorEngine
{
    public class RiskScoreCalculatorBuilderTest
    {
        private readonly RiskScoreCalculatorBuilder<PersonalProfile> builder;

        public RiskScoreCalculatorBuilderTest()
        {
            builder = new();
        }

        [Fact]
        public void AddCalculator_Should_AddRiskScoreCalculator()
        {
            // Arrange
            var calculator = new Mock<IRiskScoreCalculator<PersonalProfile>>();

            // Act
            builder.AddCalculator(calculator.Object);

            // Assert
            Assert.Contains(builder.Calculators, p => p == calculator.Object);
            Assert.Single(builder.Calculators);
        }

        [Fact]
        public void AddCalculator_Should_AddThePredicateCalculator()
        {
            // Act
            builder.AddCalculator(_ => true, new RiskScore());

            // Assert
            var calculator = builder.Calculators.First();
            Assert.IsType<PredicateCalculator<PersonalProfile>>(calculator);
        }

        [Fact]
        public void Build_Should_CreateARiskScoreCalculator_WithAllDefinedCalculators()
        {
            // Arrange
            var calculator1 = new Mock<IRiskScoreCalculator<PersonalProfile>>();
            var calculator2 = new Mock<IRiskScoreCalculator<PersonalProfile>>();

            builder.AddCalculator(calculator1.Object);
            builder.AddCalculator(calculator2.Object);

            // Act
            var builtCalculator = (RiskScoreCalculator<PersonalProfile>)builder.Build();

            // Assert
            Assert.Contains(builtCalculator.Calculators, p => p == calculator1.Object);
            Assert.Contains(builtCalculator.Calculators, p => p == calculator2.Object);
        }
    }
}
