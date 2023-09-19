using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POC_ImportZip.Models;
using POC_ImportZip.Services.Interfaces;
using System.Net;

namespace POC_ImportZip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileZipService _zipService;

        public FilesController(IFileZipService zipService)
        {
            _zipService = zipService;
        }

        /// <summary>
        /// Retorna algumas informações da imagem para validação
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Resolução da imagem (width e height) e tamanho da imagem (length)</returns>
        [HttpPost("zip")]
        public async Task<IActionResult> ImportZip([FromForm] ValidateZipViewModel file)
        {
            try
            {
                var result = await _zipService.ImportZip(file.File);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
