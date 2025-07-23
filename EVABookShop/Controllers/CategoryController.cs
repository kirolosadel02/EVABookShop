using Microsoft.AspNetCore.Mvc;
using EVABookShop.Models;
using EVABookShop.Services.Categories;

namespace EVABookShop.Controllers
{
    [Route("categories")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("")]
        public IActionResult GetAllCategories() => 
            View(_categoryService.GetAllCategoryViewModels());

        [HttpGet("create")]
        public IActionResult CreateCategory() => 
            View();

        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory(CategoryViewModel model)
        {
            var result = await _categoryService.CreateCategory(model, ModelState);
            if (result)
                return RedirectToAction("GetAllCategories");
            return View(model);
        }
    }
} 