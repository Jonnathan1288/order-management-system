using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Interfaces.Public;

public interface IShoppingCartService
{
    Task<ShoppingCart> CreateAsync(short businessId, int customerId, ShoppingCart shoppingCart);

    Task<ShoppingCart?> UpdateStatusAsync(short businessId, int customerId, int shoppingCartId, string status);
}
