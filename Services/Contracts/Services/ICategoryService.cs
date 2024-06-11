using Domain.Entities;
using Services.Models.Category;

namespace Services.Contracts.Services
{
    public interface ICategoryService
    {
        Task<CategoryForView> GetAllCategorys();
        Task<bool> UpdateCategory(CategoryForUpdate categoryDto,int id);
        Task<CategoryForViewItems> GetCategoryById(int id);
        Task<Category> AddCategory(CategoryForCreate categoryDto);
        Task<bool> DeleteCategory(int id);

    }
}
