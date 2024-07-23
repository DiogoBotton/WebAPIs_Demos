namespace POC_AzureBlobStorageSAS.Services.Interfaces;

public interface IBlobStorageService
{
    Task<string> GetBlobSasUri(string blobName);
}
