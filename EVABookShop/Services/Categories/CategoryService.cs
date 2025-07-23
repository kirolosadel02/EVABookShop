using DataAccess;
using EVABookShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return _context.Categories.Select(c => new CategoryViewModel
            {
                CatName = c.CatName,
                CatOrder = c.CatOrder,
                IsActive = !c.MarkedAsDeleted
            }).ToList();
        }

        public async Task<bool> CreateCategory(CategoryViewModel model, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                return false;

            var category = new Category
            {
                CatName = model.CatName,
                CatOrder = model.CatOrder,
                MarkedAsDeleted = !model.IsActive
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}