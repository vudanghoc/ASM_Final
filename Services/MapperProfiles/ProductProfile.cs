using AutoMapper;
using Domain.Entities;
using Services.MapperProfiles.Converters;
using Services.Models.Product;

namespace Services.MapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            Init();
        }

        private void Init()
        {
            CreateMap<Product, ProductForViewItems>()
                //.ForMember(d => d.TeamId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category.Name));

            CreateMap<IEnumerable<Product>, IEnumerable<ProductForView>>()
                .ConvertUsing<ProductConverter>();
            CreateMap<ProductForUpdate, Product>().ForMember(src => src.Id, opt => opt.Ignore());
            CreateMap<ProductForCreate, Product>();
        }

    }
}
