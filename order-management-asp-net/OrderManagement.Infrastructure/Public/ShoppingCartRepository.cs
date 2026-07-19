using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Connections;

namespace OrderManagement.Infrastructure.Public;

public class ShoppingCartRepository(PostgreSQLContext _context) : IShoppingCartRepository
{
    public async Task<ShoppingCart> CreateAsync(ShoppingCart shoppingCart)
    {
        _context.ShoppingCarts.Add(shoppingCart);
        await _context.SaveChangesAsync();
        return shoppingCart;
    }

    public Task<ShoppingCart?> FindAsync(short businessId, int customerId, int shoppingCartId)
    {
        return _context.ShoppingCarts
            .FirstOrDefaultAsync(shoppingCart => shoppingCart.BusinessId == businessId
                && shoppingCart.CustomerId == customerId
                && shoppingCart.Id == shoppingCartId);
    }

    public async Task CancelActiveAsync(short businessId, int customerId, int? exceptShoppingCartId = null)
    {
        List<ShoppingCart> shoppingCarts = await _context.ShoppingCarts
            .Where(shoppingCart => shoppingCart.BusinessId == businessId
                && shoppingCart.CustomerId == customerId
                && shoppingCart.Status == "ACTIVE"
                && (!exceptShoppingCartId.HasValue || shoppingCart.Id != exceptShoppingCartId.Value))
            .ToListAsync();

        foreach (ShoppingCart shoppingCart in shoppingCarts)
        {
            shoppingCart.Status = "CANCELLED";
        }

        await _context.SaveChangesAsync();
    }

    public async Task<ShoppingCart> UpdateAsync(ShoppingCart shoppingCart)
    {
        _context.ShoppingCarts.Update(shoppingCart);
        await _context.SaveChangesAsync();
        return shoppingCart;
    }
}
