using AutoMapper;
using DataAccess.DataAccess;
using DataAccess.DataAccess.Base;
using Services.Contracts.DataAccess;
using Services.Contracts.DataAccess.Base;
using Services.Contracts.Services;
using Services.MapperProfiles;
using Services.Services;

namespace WebAPI.Services.Extensions
{
    public static class RegisterServiceExtension
    {
        public static IServiceCollection AddRegistrationService(this IServiceCollection services)
        {
            //DataAccess
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryDataAccess, CategoryDataAccess>();
            services.AddScoped<IProductDataAccess, ProductDataAccess>();
            services.AddScoped<IComboDataAccess, ComboDataAccess>();
            services.AddScoped<IOrderDataAccess, OrderDataAccess>();
            services.AddScoped<IOrderDetailDataAccess, OrderDetailDataAccess>();
            //Service
            services.AddScoped<IUserServices, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IComboService, ComboService>();
            services.AddScoped<IOrderService, OrderService>();
            //mapper
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<ProductProfile>();
                c.AddProfile<CategoryProfile>();
                c.AddProfile<OrderProfile>();
                c.AddProfile<ComboProfile>();
            });

            services.AddSingleton<IMapper>(s => config.CreateMapper());
            
            return services;
        }
    }
}
