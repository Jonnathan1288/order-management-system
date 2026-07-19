

using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Public;

namespace OrderManagement.API.Extensions;

public static class DependencyInjectionServiceRepository
{
    // Repositories
    public static IServiceCollection AddAppRespositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBusinessRepository, BusinessRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
        services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

        return services;
    }
}
