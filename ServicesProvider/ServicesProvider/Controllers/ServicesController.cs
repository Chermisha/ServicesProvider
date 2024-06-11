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
        private readonly IServiceCategoriesService _categoriesService;

        public ServicesController(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Service>>> GetAllServices()
        {
            var services = await _servicesService.GetAllServices();
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            var service = await _servicesService.GetServiceById(id);

            return View();//дотелать
        }

        [HttpPost]
        public async Task<ActionResult> Create(string name, string? description, decimal price, int categoryId)
        {
            await _servicesService.AddService(name, description, price, categoryId);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update (int id, string name, string? description, decimal price, int categoryId)
        {
            await _servicesService.UpdateService(id, name, description, price, categoryId);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete (int id)
        {
            await _servicesService.DeleteService(id);
            return Ok();
        }
    }
}
