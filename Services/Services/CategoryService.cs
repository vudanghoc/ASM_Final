using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Services.Contracts.DataAccess;
using Services.Contracts.Services;
using Services.Models.Category;

namespace Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDataAccess _categoryDataAccess;
        private readonly ILogger<CategoryService> _logger;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryDataAccess categoryDataAccess, ILogger<CategoryService> logger, IMapper mapper)
        {
            _categoryDataAccess = categoryDataAccess;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Category> AddCategory(CategoryForCreate categoryDto)
        {
            try
            {
                Category category = _mapper.Map<Category>(categoryDto);
                var response = await _categoryDataAccess.AddCategory(category);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null!;
        }
        public async Task<bool> DeleteCategory(int id)
        {
            try
            {
                Category category = await _categoryDataAccess.GetCategoryById(id);
                if (category != null)
                {
                    _categoryDataAccess.DeleteCategory(category);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return false!;
        }

        public async Task<CategoryForView> GetAllCategorys()
        {
            try
            {
                IEnumerable<Category> categories = await _categoryDataAccess.GetAllCategorys();
                IList<CategoryForViewItems> items = _mapper.Map<IEnumerable<CategoryForViewItems>>(categories).ToList();

                CategoryForView response = new CategoryForView();
                response.Categories = items;
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null!;
        }

        public async Task<CategoryForViewItems> GetCategoryById(int id)
        {
            try
            {
                Category category = await _categoryDataAccess.GetCategoryById(id);
                CategoryForViewItems items = _mapper.Map<CategoryForViewItems>(category);
                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null!;
        }
        public async Task<bool> UpdateCategory(CategoryForUpdate categoryDto, int id)
        {
            try
            {
                Category category = await _categoryDataAccess.GetCategoryById(id);
                if(category != null)
                {
                    _mapper.Map(categoryDto, category);
                    _categoryDataAccess.UpdateCategory(category);
                    return true;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return false!;
        }
    }
}
