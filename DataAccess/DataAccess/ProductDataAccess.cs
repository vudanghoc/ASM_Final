using DataAccess.DataAccess.Base;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services.Contracts.DataAccess;

namespace DataAccess.DataAccess
{
    public class ProductDataAccess : GeneralDataAccess<Product>, IProductDataAccess
    {
        public ProductDataAccess(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<Product> AddProduct(Product product)
        {
            return await AddAsync(product);
        }

        public bool DeleteProduct(Product product)
        {
            Delete(product);
            return true;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
          return await _context.Products.Include(x=>x.Category).ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.Include(x=>x.Category).SingleOrDefaultAsync(x => x.Id == id);
        }

        public Product UpdateProduct(Product product)
        {
            return Update(product);
        }
    }
}
