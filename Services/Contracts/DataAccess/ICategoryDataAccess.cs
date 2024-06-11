using Domain.Entities;
using Services.Contracts.DataAccess.Base;

namespace Services.Contracts.DataAccess
{
    public interface ICategoryDataAccess : IGeneralDataAcces<Category>
    {
        Task<IEnumerable<Category>> GetAllCategorys();
        Category UpdateCategory(Category category);
        Task<Category> GetCategoryById(int id);
        Task<Category> AddCategory(Category category);
        bool DeleteCategory(Category category);

    }
}
