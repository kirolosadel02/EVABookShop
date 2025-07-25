using DataAccess;
using EVABookShop.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EVABookShop.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly BookShopContext _context;

        public CategoryService(BookShopContext context)
        {
            _context = context;
        }

        public List<CategoryViewModel> GetAllCategoryViewModels()
        {
            return _context.Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    CatName = c.CatName,
                    CatOrder = c.CatOrder,
                    IsActive = !c.MarkedAsDeleted
                }).ToList();
        }

        public async Task<bool> CreateCategory(CategoryViewModel model, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                return false;

            // Check if category name already exists (including soft-deleted ones)
            var existingCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.CatName == model.CatName);

            if (existingCategory != null)
            {
                if (existingCategory.MarkedAsDeleted)
                {
                    // Restore the soft-deleted category as active
                    existingCategory.CatOrder = model.CatOrder;
                    existingCategory.MarkedAsDeleted = false; // Always create as active
                }
                else
                {
                    modelState.AddModelError("CatName", "Category name already exists.");
                    return false;
                }
            }
            else
            {
                // Create new category - always active by default
                var category = new Category
                {
                    CatName = model.CatName,
                    CatOrder = model.CatOrder,
                    MarkedAsDeleted = false // Always create as active
                };
                _context.Categories.Add(category);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CategoryViewModel> GetCategoryById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return null;

            return new CategoryViewModel
            {
                Id = category.Id,
                CatName = category.CatName,
                CatOrder = category.CatOrder,
                IsActive = !category.MarkedAsDeleted
            };
        }

        public async Task<bool> UpdateCategory(int id, CategoryViewModel model, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                return false;

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
                return false;

            if (category.CatName != model.CatName)
            {
                var nameExists = await _context.Categories
                    .AnyAsync(c => c.CatName == model.CatName && c.Id != id && !c.MarkedAsDeleted);

                if (nameExists)
                {
                    modelState.AddModelError("CatName", "Category name already exists.");
                    return false;
                }
            }

            category.CatName = model.CatName;
            category.CatOrder = model.CatOrder;
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return false;

            category.MarkedAsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}