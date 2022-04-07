namespace RiskProfile.Domain.Models.PersonalProfileInformation
{
    public class PersonalProfile
    {
        public int Age { get; set; }
        public int Dependents { get; set; }
        public decimal Income { get; set; }
        public House? House { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public int[] RiskQuestions { get; set; }
        public Vehicle? Vehicle { get; set; }

        public PersonalProfile(
            int age,
            int dependents,
            House house,
            decimal income,
            MaritalStatus maritalStatus,
            int[] riskQuestions,
            Vehicle vehicle)
        {
            Age = age;
            Dependents = dependents;
            House = house;
            Income = income;
            MaritalStatus = maritalStatus;
            RiskQuestions = riskQuestions;
            Vehicle = vehicle;
        }
    }
}
