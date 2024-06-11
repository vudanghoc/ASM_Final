using Domain.Entities;
using Services.Models.Product;

namespace Services.Contracts.Services
{
    public interface IProductService
    {
        Task<ProductForView> GetAllProducts();
        Task<bool> UpdateProduct(ProductForUpdate productDto, int id);
        Task<ProductForViewItems> GetProductById(int id);
        Task<Product> AddProduct(ProductForCreate productDto);
        Task<bool> DeleteProduct(int id);

    }
}
