namespace RiskProfileCalculator.Application.RequestValidators
{
    public static class ErrorTypes
    {
        public static Error AgeShouldNotBeNull = new Error("AgeShouldNotBeNull", "Age should be informed");
        public static Error AgeShouldBeEqualOrGreaterThan0 = new Error("AgeShouldBeEqualOrGreaterThan0", "Age should be equal or greater than 0");

        public static Error DependentsShouldNotBeNull = new Error("DependentsShouldNotBeNull", "Dependents should be informed");
        public static Error DependentsShouldBeEqualOrGreaterThan0 = new Error("DependentsShouldBeEqualOrGreaterThan0", "Dependents should be equal or greater than 0");

        public static Error IncomeShouldNotBeNull = new Error("IncomeShouldNotBeNull", "Income should be informed");
        public static Error IncomeShouldBeEqualOrGreaterThan0 = new Error("IncomeShouldBeEqualOrGreaterThan0", "Income should be equal or greater than 0");

        public static Error MaritalStatusShouldNotBeNull = new Error("MaritalStatusShouldNotBeNull", "Income should be informed");
        public static Error MaritalStatusShouldBeValid = new Error("MaritalStatusShouldBeValid", "Marital status should be valid. The valid values are [{0}]");

        public static Error VehicleYearShouldBeGreaterThan0 = new Error("VehicleYearShouldBeGreaterThan0", "Vehicle year should be greater than 0");

        public static Error RiskQuestionsShouldBeValid = new Error("RiskQuestionsShouldBeValid", "All the risk questions should be answered and be binary");

        public static Error HouseOwnershipStatusMustBeValid = new Error("HouseOwnershipStatusMustBeValid", "House ownership status must be valid. The valid values are [{0}]");
    }
}
