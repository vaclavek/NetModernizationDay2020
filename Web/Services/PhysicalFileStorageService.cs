using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Services
{
    public class PhysicalFileStorageService : IFileStorageService
    {
        public Task<IEnumerable<string>> EnumerateFilesAsync(string path)
        {
            var di = new DirectoryInfo(path);
            return Task.FromResult(di.GetFiles().Select(x => x.Name) as IEnumerable<string>);
        }

        public Task<bool> ExistsAsync(string filePath)
        {
            return Task.FromResult(File.Exists(filePath));
        }

        public Task<Stream> GetFileAsync(string fileName)
        {
            return Task.FromResult(File.OpenRead(fileName) as Stream);
        }
    }
}
