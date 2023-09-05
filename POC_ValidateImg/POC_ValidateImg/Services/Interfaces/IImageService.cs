using POC_ValidateImg.Models;

namespace POC_ValidateImg.Services.Interfaces;

public interface IImageService
{
    Task<InfosImage> ValidateImageSystemDrawing(IFormFile file);
    Task<InfosImage> ValidateImageImageSharp(IFormFile file);
}
