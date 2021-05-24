using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.DotNetCore3.Inputs;
using WebApi.DotNetCore3.Interfaces;

namespace WebApi.DotNetCore3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public IUsuarioRepository _usuarioRepository { get; set; }

        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginInput input)
        {
            try
            {
                var retorno = _usuarioRepository.GetUsuarioByEmailSenha(input.Email, input.Senha);

                if (retorno == null)
                    return NotFound("Nome e/ou senha inválidos.");

                var informacoesUsuario = new[]
                {
                new Claim(JwtRegisteredClaimNames.Email, retorno.Email),
                new Claim(JwtRegisteredClaimNames.Jti, retorno.Id.ToString()), // Jti claimName para ID's
            };

                // Define a chave de acesso ao token
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("produtos-chave-autenticacao"));

                // Define as credenciais do token - Header
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Gera o token
                var token = new JwtSecurityToken(
                    issuer: "WebApi.DotNetCore3.Produtos",         // emissor do token
                    audience: "WebApi.DotNetCore3.Produtos",       // destinatário do token
                    claims: informacoesUsuario,             // dados definidos acima
                    expires: DateTime.Now.AddMinutes(30),   // tempo de expiração
                    signingCredentials: creds               // credenciais do token
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new List<string>()
                {
                    "Ocorreu algum erro no Login de Usuário.",
                     ex.Message
                });
            }
        }
    }
}
