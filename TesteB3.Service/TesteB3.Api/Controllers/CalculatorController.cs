using Microsoft.AspNetCore.Mvc;
using TesteB3.Domain.Interfaces;
using TesteB3.Domain.ViewModels;

namespace TesteB3.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController(ICdbService cdbService) : ControllerBase
    {
        private readonly ICdbService cdbService = cdbService;

        [HttpPost("cdb")]
        public IActionResult ComputeCdb([FromBody] CdbViewModel cdbViewModel)
        {
            try
            {
                return Ok(cdbService.ComputeCdbValue(cdbViewModel));
            }
            catch (Exception ex)
            {
                var messages = ex.Message.Split(';');
                return BadRequest(new { messages });
            }
        }
    }
}
