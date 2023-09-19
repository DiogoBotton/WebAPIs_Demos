using POC_ImportZip.Models;

namespace POC_ImportZip.Services.Interfaces;

public interface IFileZipService
{
    Task<List<InfosZip>> ImportZip(IFormFile file);
}
