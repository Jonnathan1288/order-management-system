using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Interfaces;

public interface IShoppingCartRepository
{
    Task<ShoppingCart> CreateAsync(ShoppingCart shoppingCart);

    Task<ShoppingCart?> FindAsync(short businessId, int customerId, int shoppingCartId);

    Task CancelActiveAsync(short businessId, int customerId, int? exceptShoppingCartId = null);

    Task<ShoppingCart> UpdateAsync(ShoppingCart shoppingCart);
}
