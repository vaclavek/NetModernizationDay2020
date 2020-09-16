using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        public async Task<IActionResult> GetFile(string fileName)
        {
            var fi = new FileInfo(Path.Combine("Files", fileName));
            var file = await System.IO.File.ReadAllBytesAsync(fi.FullName);
            return new FileContentResult(file, "application/octet-stream")
            {
                FileDownloadName = fileName
            };
        }
    }
}
