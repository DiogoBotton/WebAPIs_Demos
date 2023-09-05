using POC_ValidateImg.Models;
using POC_ValidateImg.Services.Interfaces;
using System.Drawing;

namespace POC_ValidateImg.Services;

public class ImageService : IImageService
{
    public async Task<InfosImage> ValidateImage(IFormFile file)
    {
        using (var fileImg = file.OpenReadStream())
        {
            using (var img = Image.FromStream(fileImg))
            {
                return await Task.FromResult(new InfosImage(img.Width, img.Height, fileImg.Length));
            }
        }
    }
}
