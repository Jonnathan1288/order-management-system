using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Connections;

namespace OrderManagement.Infrastructure.Public;

public class OrderRepository(PostgreSQLContext _context) : IOrderRepository
{
    public async Task<Order> CreateAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public Task<List<Order>> FindByBusinessAsync(short businessId)
    {
        return _context.Orders
            .AsNoTracking()
            .Include(order => order.OrderDetails)
            .Include(order => order.PaymentMethod)
            .Where(order => order.BusinessId == businessId)
            .ToListAsync();
    }

    public Task<Order?> FindByBusinessAndCustomerAsync(short businessId, int customerId, int orderId)
    {
        return _context.Orders
            .AsNoTracking()
            .Include(order => order.OrderDetails)
            .Include(order => order.PaymentMethod)
            .FirstOrDefaultAsync(order => order.BusinessId == businessId
                && order.CustomerId == customerId
                && order.Id == orderId);
    }
}
