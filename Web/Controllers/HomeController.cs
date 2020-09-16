using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileStorageService fileStorageService;

        public HomeController(IFileStorageService fileStorageService)
        {
            this.fileStorageService = fileStorageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ViewResult> Files()
        {
            var files = await fileStorageService.EnumerateFilesAsync("Files");
            return View(files.ToArray());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
