using EVABookShop.Models;
using EVABookShop.UnitOfWork;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models;

namespace EVABookShop.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<CategoryViewModel> GetAllCategoryViewModels()
        {
            var categories = _unitOfWork.Repository<Category>().GetAll().Result;
            return categories.Select(c => new CategoryViewModel
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

            var repo = _unitOfWork.Repository<Category>();
            var existingCategory = (await repo.GetData(c => c.CatName == model.CatName)).FirstOrDefault();

            if (existingCategory != null)
            {
                if (existingCategory.MarkedAsDeleted)
                {
                    existingCategory.CatOrder = model.CatOrder;
                    existingCategory.MarkedAsDeleted = false;
                    await repo.Update(existingCategory);
                }
                else
                {
                    modelState.AddModelError("CatName", "Category name already exists.");
                    return false;
                }
            }
            else
            {
                var category = new Category
                {
                    CatName = model.CatName,
                    CatOrder = model.CatOrder,
                    MarkedAsDeleted = false
                };
                repo.Add(category);
            }

            await _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<CategoryViewModel> GetCategoryById(int id)
        {
            var category = await Task.Run(() => _unitOfWork.Repository<Category>().GetById(id));
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

            var repo = _unitOfWork.Repository<Category>();
            var category = await Task.Run(() => repo.GetById(id));
            if (category == null)
                return false;

            if (category.CatName != model.CatName)
            {
                var nameExists = (await repo.GetData(c => c.CatName == model.CatName && c.Id != id && !c.MarkedAsDeleted)).Any();
                if (nameExists)
                {
                    modelState.AddModelError("CatName", "Category name already exists.");
                    return false;
                }
            }

            category.CatName = model.CatName;
            category.CatOrder = model.CatOrder;
            await repo.Update(category);
            await _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var repo = _unitOfWork.Repository<Category>();
            var category = await Task.Run(() => repo.GetById(id));
            if (category == null) return false;

            category.MarkedAsDeleted = true;
            await repo.Update(category);
            await _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> CheckCategoryNameExistsAsync(string categoryName, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return false;

            var repo = _unitOfWork.Repository<Category>();
            var query = await repo.GetData(c => c.CatName.ToLower().Trim() == categoryName.ToLower().Trim() && !c.MarkedAsDeleted);
            if (excludeId.HasValue)
                query = query.Where(c => c.Id != excludeId.Value);

            return query.Any();
        }

        public async Task<bool> CheckCategoryOrderExistsAsync(int categoryOrder, int? excludeId = null)
        {
            var repo = _unitOfWork.Repository<Category>();
            var query = await repo.GetData(c => c.CatOrder == categoryOrder && !c.MarkedAsDeleted);
            if (excludeId.HasValue)
                query = query.Where(c => c.Id != excludeId.Value);

            return query.Any();
        }
    }
}
