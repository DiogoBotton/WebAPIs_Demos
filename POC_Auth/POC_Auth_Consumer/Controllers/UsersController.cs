using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace POC_Auth_Consumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult List()
        {
            try
            {
                return Ok("Deu bom!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Houve um erro interno ao dar bom.");
            }
        }
    }
}
