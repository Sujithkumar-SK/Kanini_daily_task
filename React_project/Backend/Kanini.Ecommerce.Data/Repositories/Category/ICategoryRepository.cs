using Kanini.Ecommerce.Common;
using Kanini.Ecommerce.Domain.Entities;

namespace Kanini.Ecommerce.Data.Repositories.Categories;

public interface ICategoryRepository
{
    // ADO.NET Read Operations
    Task<Result<List<Category>>> GetAllCategoriesAsync();
    Task<Result<Category>> GetCategoryByIdAsync(int categoryId);
    Task<Result<bool>> IsCategoryNameExistsAsync(string name, int? categoryId = null);
    Task<Result<bool>> ValidateCategoryAsync(int categoryId);

    // EF Core Write Operations
    Task<Result<Category>> CreateCategoryAsync(Category category);
    Task<Result> UpdateCategoryAsync(Category category);
    Task<Result> DeleteCategoryAsync(int categoryId, string deletedBy);
}
