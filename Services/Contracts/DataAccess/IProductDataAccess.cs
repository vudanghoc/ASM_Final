using Domain.Entities;
using Services.Contracts.DataAccess.Base;

namespace Services.Contracts.DataAccess
{
    public interface IProductDataAccess : IGeneralDataAcces<Product>
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Product UpdateProduct(Product product);
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(Product product);
        bool DeleteProduct(Product product);

    }
}
