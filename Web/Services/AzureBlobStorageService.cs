using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;

namespace Web.Services
{
    public class AzureBlobStorageService : IFileStorageService
    {
        private readonly AzureBlobStorage options;

        public AzureBlobStorageService(IOptions<AzureBlobStorage> options)
        {
            this.options = options.Value;
        }

        public async Task<IEnumerable<string>> EnumerateFilesAsync(string path)
        {
            var c = new BlobContainerClient(this.options.ConnectionString, this.options.ContainerName);

            List<string> names = new List<string>();
            await foreach (BlobItem blob in c.GetBlobsAsync(prefix: path))
            {
                names.Add(blob.Name);
            }
            return names;
        }

        public async Task<bool> ExistsAsync(string fileName)
        {
            var response = await GetClient(fileName).ExistsAsync();
            return response.Value;
        }

        public async Task<Stream> GetFileAsync(string fileName)
        {
            var ms = new MemoryStream();
            BlobDownloadInfo download = await GetClient(fileName).DownloadAsync();
            await download.Content.CopyToAsync(ms);
            await ms.FlushAsync();
            return ms;
        }

        private BlobClient GetClient(string fileName)
        {
            return new BlobClient(this.options.ConnectionString, this.options.ContainerName, fileName);
        }
    }
}
