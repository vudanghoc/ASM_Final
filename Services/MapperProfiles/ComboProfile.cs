using AutoMapper;
using Domain.Entities;
using Services.Models.Combo;

namespace Services.MapperProfiles
{
    public class ComboProfile : Profile
    {
        public ComboProfile()
        {
            Init();
        }

        private void Init()
        {
            CreateMap<Combo, ComboForView>();

            CreateMap<Combo, ComboForViewItems>();
                //.ForMember(c => c.ProductCombos, p => p.MapFrom(s => s.ProductCombos));
            CreateMap<ProductCombo, ProductComboInfoForView>()
                .ForMember(c => c.ProductName, p => p.MapFrom(s => s.Product.Name))
                .ForMember(c => c.ProductPrice, p => p.MapFrom(s => s.Product.Price))
                .ForMember(c=>c.ProductId, p=>p.MapFrom(s => s.Product.Id));

            CreateMap<ComboForCreate, Combo>()
                .ForMember(src => src.Id, opt => opt.MapFrom(x=>new int()))
                .ForMember(src => src.ProductCombos, opt => opt.Ignore());
           
            CreateMap<ProductComboInfoForCreate, ProductCombo>()
                .ForMember(src => src.ComboId, opt => opt.Ignore())
                .ForMember(src => src.Combo, opt => opt.Ignore());

            CreateMap<ComboForUpdate, Combo>()
                .ForMember(src => src.Id, opt => opt.Ignore());
            CreateMap<ProductComboInfoForUpdate,ProductCombo>()
                .ForMember(src => src.ComboId, opt => opt.Ignore())
                .ForMember(src => src.Combo, opt => opt.Ignore());
        }
    }
}
