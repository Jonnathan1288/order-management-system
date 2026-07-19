
using OrderManagement.Application.Interfaces.Custom;
using OrderManagement.Application.Interfaces.Public;
using OrderManagement.Application.Services.Custom;
using OrderManagement.Application.Services.Public;

namespace OrderManagement.API.Extensions;

public static class DependencyInjectionService
{
    // Services
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBusinessService, BusinessService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPaymentMethodService, PaymentMethodService>();
        services.AddScoped<IShoppingCartService, ShoppingCartService>();

        // Singleton
        services.AddSingleton<IJWTService, JWTService>();
        return services;
    }
}
