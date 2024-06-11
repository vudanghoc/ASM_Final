using DataAccess.DataAccess.Base;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services.Contracts.DataAccess;

namespace DataAccess.DataAccess
{
    public class CategoryDataAccess : GeneralDataAccess<Category>, ICategoryDataAccess
    {
        public CategoryDataAccess(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<Category> AddCategory(Category category)
        {

            return await AddAsync(category);
        }

        public bool DeleteCategory(Category category)
        {
            Delete(category);
            return true;
        }

        public async Task<IEnumerable<Category>> GetAllCategorys()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.SingleOrDefaultAsync(x => x.Id == id);
        }

        public Category UpdateCategory(Category category)
        {
            return Update(category);
        }
    }
}
