using POC_ValidateImg.Models;

namespace POC_ValidateImg.Services.Interfaces;

public interface IImageService
{
    Task<InfosImage> ValidateImage(IFormFile file);
}
