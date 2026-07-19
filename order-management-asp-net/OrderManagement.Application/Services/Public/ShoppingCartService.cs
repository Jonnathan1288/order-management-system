using OrderManagement.Application.Interfaces.Public;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Services.Public;

public class ShoppingCartService(IShoppingCartRepository _repository) : IShoppingCartService
{
    public async Task<ShoppingCart> CreateAsync(short businessId, int customerId, ShoppingCart shoppingCart)
    {
        await _repository.CancelActiveAsync(businessId, customerId);

        shoppingCart.BusinessId = businessId;
        shoppingCart.CustomerId = customerId;
        shoppingCart.Status = "ACTIVE";

        return await _repository.CreateAsync(shoppingCart);
    }

    public async Task<ShoppingCart?> UpdateStatusAsync(short businessId, int customerId, int shoppingCartId, string status)
    {
        ShoppingCart? shoppingCart = await _repository.FindAsync(businessId, customerId, shoppingCartId);
        if (shoppingCart is null) return null;

        if (status == "ACTIVE")
        {
            await _repository.CancelActiveAsync(businessId, customerId, shoppingCartId);
        }

        shoppingCart.Status = status;
        return await _repository.UpdateAsync(shoppingCart);
    }
}
