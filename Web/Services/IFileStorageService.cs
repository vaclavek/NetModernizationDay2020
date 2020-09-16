using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Web.Services
{
    public interface IFileStorageService
    {
        Task<bool> ExistsAsync(string filePath);
        Task<IEnumerable<string>> EnumerateFilesAsync(string path);
        Task<Stream> GetFileAsync(string fileName);
    }
}
