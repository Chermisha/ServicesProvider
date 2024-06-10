using Microsoft.AspNetCore.Mvc;
using ServicesProvider.Application.Services;
using ServicesProvider.Domain.Models;
using ServicesProvider.Models;

namespace ServicesProvider.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesController : Controller
    {
        private readonly IServicesService _servicesService;
        public ServicesController(IServicesService servicesService) 
        {
            _servicesService = servicesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Service>>> AllServices()
        {
            var services = await _servicesService.GetAllService();
            var serviceViewModels = services.Select(s => new ServiceViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Price = s.Price,
                Category = s.Category.Name.ToString()
            }).ToList();
            return View("AllServices", serviceViewModels);
        }
    }
}
