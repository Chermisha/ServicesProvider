using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ServicesProvider.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Policy = "ClientPolicy")]
        public async Task<ActionResult> Client()
        {
            return View("Client");
        }

        [HttpGet]
        [Authorize(Policy = "ProviderPolicy")]
        public async Task<ActionResult> Provider()
        {
            return View("Provider");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
