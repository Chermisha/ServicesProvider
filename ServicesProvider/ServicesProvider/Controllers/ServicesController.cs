using Microsoft.AspNetCore.Authorization;
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

        public ServicesController(IServicesService servicesService, IServiceCategoriesService categoriesService)
        {
            _servicesService = servicesService;
            _categoriesService = categoriesService;
        }

        [Authorize(Policy = "ClientPolicy")]
        [HttpGet]
        public async Task<ActionResult> GetAllServices()
        {
            return View("AllServices", await GetAllServiceViewModels());
        }

        [Authorize(Policy = "ProviderPolicy")]
        [HttpGet("edit")]
        public async Task<ActionResult> GetAllServicesWithEdit()
        {
            return View("AllServicesWithEdit", await GetAllServiceViewModels());
        }

        [Authorize(Policy = "ProviderPolicy")]
        [HttpGet("create")]
        public async Task<ActionResult<Service>> Create ()
        {
            ViewBag.Categories = await GetAllCategoryViewModels();

            return View("Create");
        }

        [Authorize(Policy = "ProviderPolicy")]
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromForm] string name, [FromForm] string? description, [FromForm] decimal price, [FromForm] string category)
        {
            await _servicesService.AddService(name, description, price, int.Parse(category));
            return RedirectToAction("GetAllServicesWithEdit");
        }

        [Authorize(Policy = "ProviderPolicy")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> Update( int id)
        {
            var service = await _servicesService.GetServiceById(id);

            if (service == null)
            {
                return BadRequest("Не найдена услуга");
            }

            var serviceViewModel = new ServiceViewModel
            {
                Id = id,
                Name = service.Name,
                Description = service.Description ?? string.Empty,
                Price = service.Price,
                Category = service.Category.Name.ToString()
            };

            ViewBag.Categories = await GetAllCategoryViewModels();

            return View("Update", serviceViewModel);
        }

        [Authorize(Policy = "ProviderPolicy")]
        [HttpPost("{id}")]
        public async Task<ActionResult> Update ([FromRoute] int id, [FromForm] string name, [FromForm] string? description, [FromForm]decimal price, [FromForm] string category)
        {
            await _servicesService.UpdateService(id, name, description, price, int.Parse(category));
            return RedirectToAction("GetAllServicesWithEdit");
        }

        [HttpGet("delete")]
        public async Task<ActionResult> Delete (int id)
        {
            await _servicesService.DeleteService(id);
            return RedirectToAction("GetAllServicesWithEdit");
        }

        [NonAction]
        private async Task<List<ServiceViewModel>> GetAllServiceViewModels ()
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

            return serviceViewModels;
        }

        [NonAction]
        private async Task<List<CategoryViewModel>> GetAllCategoryViewModels()
        {
            var category = await _categoriesService.GetAllServiceCategories();
            if (category == null || category.Count == 0)
            {
                return null;
            }

            var categoryViewModels = category.Select(s => new CategoryViewModel
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            return categoryViewModels;
        }
    }
}
