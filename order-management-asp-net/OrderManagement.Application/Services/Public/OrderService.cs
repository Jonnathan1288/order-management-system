using OrderManagement.Application.Interfaces.Public;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Services.Public;

public class OrderService(IOrderRepository _repository) : IOrderService
{
    public Task<Order> CreateAsync(short businessId, Order order)
    {
        order.BusinessId = businessId;
        foreach (OrderDetail orderDetail in order.OrderDetails)
        {
            orderDetail.Order = order;
        }

        return _repository.CreateAsync(order);
    }

    public Task<List<Order>> GetByBusinessAsync(short businessId)
    {
        return _repository.FindByBusinessAsync(businessId);
    }

    public Task<Order?> GetByBusinessAndCustomerAsync(short businessId, int customerId, int orderId)
    {
        return _repository.FindByBusinessAndCustomerAsync(businessId, customerId, orderId);
    }
}
