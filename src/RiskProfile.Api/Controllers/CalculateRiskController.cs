using Microsoft.AspNetCore.Mvc;
using RiskProfileCalculator.Application.Interfaces.UseCases;
using RiskProfileCalculator.Application.RequestValidators;
using RiskProfileCalculator.Application.UseCases.CalculateRiskProfile;

namespace RiskProfile.Api.Controllers
{
    [ApiController]
    [Route("calculate-risk")]
    public class CalculateRiskController : ControllerBase, ICalculateRiskProfileUseCaseOutputPort
    {
        private readonly ICalculateRiskProfileUseCase _calculateRiskProfileUseCase;
        private CalculateRiskProfileResponse response;

        public CalculateRiskController(ICalculateRiskProfileUseCase calculateRiskProfileUseCase)
        {
            _calculateRiskProfileUseCase = calculateRiskProfileUseCase;
        }


        [HttpPost]
        public IActionResult Calculate([FromBody] CalculateRiskProfileRequest request)
        {
            _calculateRiskProfileUseCase.CalculateRiskProfile(this, request);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(response);
        }

        [NonAction]
        public void NotifyValidationErrors(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(error.Code, error.Message);
            }
        }

        [NonAction]
        public void Success(CalculateRiskProfileResponse response)
        {
            this.response = response;
        }
    }
}
