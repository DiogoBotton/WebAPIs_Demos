using POC_ValidateImg.Models;
using POC_ValidateImg.Services.Interfaces;

namespace POC_ValidateImg.Services;

public class ImageService : IImageService
{
    public async Task<InfosImage> ValidateImageImageSharp(IFormFile file)
    {
        using (var fileImg = file.OpenReadStream())
        {
            using (var img = await SixLabors.ImageSharp.Image.LoadAsync(fileImg))
            {
                return new InfosImage(img.Width, img.Height, fileImg.Length);
            }
        }
    }

    public async Task<InfosImage> ValidateImageSystemDrawing(IFormFile file)
    {
        using (var fileImg = file.OpenReadStream())
        {
            using (var img = System.Drawing.Image.FromStream(fileImg))
            {
                return await Task.FromResult(new InfosImage(img.Width, img.Height, fileImg.Length));
            }
        }
    }
}
