using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POC_AzureBlobStorageSAS.Services.Interfaces;

namespace POC_AzureBlobStorageSAS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlobStorageController : ControllerBase
{private readonly IBlobStorageService _blobStorageService;

    public BlobStorageController(IBlobStorageService blobStorageService)
    {
        _blobStorageService = blobStorageService;
    }

    [HttpGet("GetTokenSas")]
    public async Task<IActionResult> GetBlobSasUri([FromQuery] string blobName)
    {
        var sasUri = await _blobStorageService.GetBlobSasUri(blobName);
        return Ok(new
        {
            url = sasUri
        });
    }
}
