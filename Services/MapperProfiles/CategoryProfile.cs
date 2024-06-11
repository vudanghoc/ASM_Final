using AutoMapper;
using Domain.Entities;
using Services.Models.Category;

namespace Services.MapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            Init();
        }

        private void Init()
        {
            CreateMap<Category, CategoryForView>();
            CreateMap<Category, CategoryForViewItems>();
            CreateMap<CategoryForUpdate, Category>().ForMember(src => src.Id, opt => opt.Ignore());
            CreateMap<CategoryForCreate, Category>();
        }
        
    }
}
