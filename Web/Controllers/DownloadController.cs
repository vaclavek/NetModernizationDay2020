using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly IFileStorageService fileStorageService;

        public DownloadController(IFileStorageService fileStorageService)
        {
            this.fileStorageService = fileStorageService;
        }

        public async Task<IActionResult> GetFile(string fileName)
        {
            var filePath = Path.Combine("Files", fileName);
            if (!await fileStorageService.ExistsAsync(filePath))
            {
                return NotFound();
            }

            var fileStream = await fileStorageService.GetFileAsync(filePath);
            var file = ReadFully(fileStream);
            return new FileContentResult(file, "application/octet-stream")
            {
                FileDownloadName = fileName
            };
        }

        // https://stackoverflow.com/a/221941
        private static byte[] ReadFully(Stream input)
        {
            input.Position = 0;
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
