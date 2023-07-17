using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using POC_Shared.Helpers;
using POC_Shared.Options;

namespace POC_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly JwtAuth _jwtAuth;

        public AuthController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _jwtAuth = new JwtAuth();
        }

        [HttpPost]
        public IActionResult Login()
        {
            try
            {
                //var jwt = _serviceProvider.GetRequiredService<IOptionsSnapshot<JwtSecrets>>().Value;

                //string token = _jwtAuth.GenerateTokenSymmetric(jwt);

                string token = _jwtAuth.GenerateTokenAsymmetric();

                return Ok(token);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao retornar o token de autenticação.");
            }
        }
    }
}
