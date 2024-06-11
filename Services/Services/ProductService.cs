using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Services.Contracts.DataAccess;
using Services.Contracts.Services;
using Services.Models.Product;

namespace Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductDataAccess _productDataAccess;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;

        public ProductService(IProductDataAccess productDataAccess, ILogger<ProductService> logger, IMapper mapper)
        {
            _productDataAccess = productDataAccess;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Product> AddProduct(ProductForCreate productDto)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDto);
                var response = await _productDataAccess.AddProduct(product);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null!;
        }
        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                Product product = await _productDataAccess.GetProductById(id);
                if (product != null)
                {
                    _productDataAccess.DeleteProduct(product);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return false!;
        }

        public async Task<ProductForView> GetAllProducts()
        {
            try
            {
                IEnumerable<Product> categories = await _productDataAccess.GetAllProducts();
                IList<ProductForViewItems> items = _mapper.Map<IEnumerable<ProductForViewItems>>(categories).ToList();

                ProductForView response = new ProductForView();
                response.Products = items;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null!;
        }

        public async Task<ProductForViewItems> GetProductById(int id)
        {
            try
            {
                Product product = await _productDataAccess.GetProductById(id);
                ProductForViewItems items = _mapper.Map<ProductForViewItems>(product);
                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null!;
        }
        public async Task<bool> UpdateProduct(ProductForUpdate productDto, int id)
        {
            try
            {
                Product product = await _productDataAccess.GetProductById(id);
                if (product != null)
                {
                    _mapper.Map(productDto, product);
                    _productDataAccess.UpdateProduct(product);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return false!;
        }
    }
}
