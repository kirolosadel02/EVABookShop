using EVABookShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EVABookShop.Services.Categories
{
    public interface ICategoryService
    {
        List<CategoryViewModel> GetAllCategoryViewModels();
        Task<bool> CreateCategory(CategoryViewModel model, ModelStateDictionary modelState);
    }
}