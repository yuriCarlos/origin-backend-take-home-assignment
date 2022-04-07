namespace RiskProfileCalculator.Application.RequestValidators
{
    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
