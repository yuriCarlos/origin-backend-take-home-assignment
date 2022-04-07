using AutoBogus;
using Bogus;
using RiskProfile.Domain.Models.PersonalProfileInformation;
using System;

namespace RiskProfile.UnitTests.Fixtures
{
    public static class PersonalProfileFixture
    {
        public static Faker<PersonalProfile> CreatePersonalProfile()
        {
            return new AutoFaker<PersonalProfile>()
                .RuleFor(p => p.Income, p => p.Random.Number(0, 1000000))
                .RuleFor(p => p.Dependents, p => p.Random.Number(0, 10))
                .RuleFor(p => p.Age, p => p.Random.Number(1, 110))
                .RuleFor(p => p.MaritalStatus, p => (MaritalStatus) p.Random.Number(1, 2))
                .RuleFor(p => p.RiskQuestions, p => new int[] { 
                    p.Random.Number(0, 1), 
                    p.Random.Number(0, 1), 
                    p.Random.Number(0, 1) })
                .RuleFor(p => p.House, AutoFaker.Generate<House>())
                .RuleFor(p => p.Vehicle, AutoFaker.Generate<Vehicle>());
        }
    }
}
