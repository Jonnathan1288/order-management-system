using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Connections;

namespace OrderManagement.Infrastructure.Public;

public class PaymentMethodRepository(PostgreSQLContext _context) : IPaymentMethodRepository
{
    public Task<List<PaymentMethod>> FindByBusinessAsync(short businessId)
    {
        return _context.PaymentMethods
            .AsNoTracking()
            .Where(paymentMethod => paymentMethod.BusinessId == businessId)
            .ToListAsync();
    }
}
