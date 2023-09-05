using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POC_ValidateImg.Models;
using POC_ValidateImg.Services.Interfaces;
using System.Net;

namespace POC_ValidateImg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        /// <summary>
        /// Retorna algumas informações da imagem para validação
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Resolução da imagem (width e height) e tamanho da imagem (length)</returns>
        [HttpPost("system-drawing")]
        public async Task<IActionResult> ValidateImageSystemDrawing([FromForm] ValidateImgViewModel file)
        {
            try
            {
                var result = await _imageService.ValidateImageSystemDrawing(file.File);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("image-sharp")]
        public async Task<IActionResult> ValidateImageSharp([FromForm] ValidateImgViewModel file)
        {
            try
            {
                var result = await _imageService.ValidateImageImageSharp(file.File);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
