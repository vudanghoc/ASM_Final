using AutoMapper;
using Domain.Entities;
using Services.Models.Order;

namespace Services.MapperProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            Init();
        }

        private void Init()
        {
            //CreateMap<Order, OrderForView>();
            CreateMap<Order, OrderForViewItems>()
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.UserName : string.Empty))
                 .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User != null ? src.User.Id : string.Empty))
                 .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));
            CreateMap<OrderDetail, OrderDetailForView>()
                 .ForMember(dest => dest.OrderDetailId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.Product.Image));

            CreateMap<OrderForUpdateStatus, Order>();
            CreateMap<OrderForUpdate, Order>()
                .ForMember(src => src.Id, opt => opt.Ignore())
                .ForMember(src => src.OrderDetails, opt => opt.Ignore());
            CreateMap<OrderDetailForUpdate, OrderDetail>()
                .ForMember(src => src.OrderId, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.Ignore());

            CreateMap<OrderForCreate, Order>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new int()))
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());

            CreateMap<OrderDetailForCreate, OrderDetail>()
                .ForMember(dest => dest.OrderId, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.Ignore());
        }
    }
}
