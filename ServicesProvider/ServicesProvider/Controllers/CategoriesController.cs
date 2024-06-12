using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesProvider.Application.Services;
using ServicesProvider.Models;

namespace ServicesProvider.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "ProviderPolicy")]
    public class CategoriesController:Controller
    {
        private readonly IServiceCategoriesService _categoriesService;

        public CategoriesController(IServiceCategoriesService categoriesService)
        {
             _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCategories()
        {
            var categories = await _categoriesService.GetAllServiceCategories();
            var categoryViewModels = categories.Select(s => new CategoryViewModel
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
            return View("AllCategories", categoryViewModels);
        }

        [HttpGet("create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromForm] string name)
        {
            await _categoriesService.AddServiceCategory(name);
            return RedirectToAction("GetAllCategories");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Update(int id)
        {
            var category = (await _categoriesService.GetServiceCategoryById(id)).Data;
            var categoryViewModel = new CategoryViewModel 
            { 
                Id = category.Id, 
                Name = category.Name
            };
            return View(categoryViewModel);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> Update ([FromRoute] int id, [FromForm] string name)
        {
            await _categoriesService.UpdateServiceCategory(id, name);
            return RedirectToAction("GetAllCategories");
        }

        [HttpGet("delete")]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            await _categoriesService.DeleteServiceCategory(id);
            return RedirectToAction("GetAllCategories");
        }

    }
}
