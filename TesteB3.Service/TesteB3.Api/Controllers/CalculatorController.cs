using Microsoft.AspNetCore.Mvc;
using TesteB3.Domain.Interfaces;

namespace TesteB3.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICdbService cdbService;

        public CalculatorController(ICdbService cdbService)
        {
            this.cdbService = cdbService;
        }

        [HttpPost("cdb")]
        public IActionResult ComputeCdb()
        {
            return new OkResult();
        }
    }
}
