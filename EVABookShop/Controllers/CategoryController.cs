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

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> EditCategory(int id)
        {
            var model = await _categoryService.GetCategoryById(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(int id, CategoryViewModel model)
        {
            if (id != model.Id)
            {
                TempData["ErrorMessage"] = "Invalid category ID.";
                return RedirectToAction("GetAllCategories");
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please correct the errors and try again.";
                return View(model);
            }

            var result = await _categoryService.UpdateCategory(id, model, ModelState);

            if (result)
            {
                TempData["SuccessMessage"] = "Category updated successfully.";
                return RedirectToAction("GetAllCategories");
            }

            TempData["ErrorMessage"] = "Failed to update category. Please check the errors and try again.";
            return View(model);
        }


        [HttpGet("details/{id}")]
        public async Task<IActionResult> DetailsCategory(int id)
        {
            var model = await _categoryService.GetCategoryById(id);
            if (model == null) return NotFound();
            return View(model);
        }


        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategory(id);
            return RedirectToAction("GetAllCategories");
        }

        [HttpPost("check-name-exists")]
        public async Task<IActionResult> CheckCategoryNameExists(string categoryName, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return Json(new { exists = false });

            var exists = await _categoryService.CheckCategoryNameExistsAsync(categoryName.Trim(), excludeId);
            return Json(new { exists });
        }

        [HttpGet("check-order-exists")]
        public async Task<IActionResult> CheckCategoryOrderExists(int categoryOrder, int? excludeId = null)
        {
            var exists = await _categoryService.CheckCategoryOrderExistsAsync(categoryOrder, excludeId);
            return Json(new { exists });
        }

    }
}