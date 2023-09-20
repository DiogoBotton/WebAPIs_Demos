using POC_ImportZip.Models;

namespace POC_ImportZip.Services.Interfaces;

public interface IFileZipService
{
    Task<List<InfosZip>> ImportZipWithSystem(IFormFile file);
    Task<List<InfosZip>> ImportZipWithSharpZipLibAndDecompressZip(IFormFile file);
}
