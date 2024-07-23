using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using POC_AzureBlobStorageSAS.Options;
using POC_AzureBlobStorageSAS.Services.Interfaces;

namespace POC_AzureBlobStorageSAS.Services;

public class BlobStorageService : IBlobStorageService
{
    public BlobStorageOptions blobStorageOptions { get; set; }
    public BlobStorageService(IOptionsSnapshot<BlobStorageOptions> blobStorageOptions)
    {
        this.blobStorageOptions = blobStorageOptions.Value;
    }

    public Task<string> GetBlobSasUri(string blobName)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(blobStorageOptions.ConnectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(blobStorageOptions.ContainerName);
        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        BlobSasBuilder sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = blobStorageOptions.ContainerName,
            BlobName = blobName,
            Resource = "b",
            ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(15)
        };

        sasBuilder.SetPermissions(BlobSasPermissions.All | BlobSasPermissions.Write);

        string sasToken = sasBuilder.ToSasQueryParameters(new StorageSharedKeyCredential(blobServiceClient.AccountName, blobStorageOptions.AccountKey)).ToString();

        UriBuilder uriBuilder = new UriBuilder(blobClient.Uri)
        {
            Query = sasToken
        };

        return Task.FromResult(uriBuilder.Uri.ToString());
    }
}
