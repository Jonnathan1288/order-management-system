using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Connections;

namespace OrderManagement.Infrastructure.Public;

public class BusinessRepository(PostgreSQLContext _context) : IBusinessRepository
{
    public Task<Business?> FindFirstAsync()
    {
        return _context.Businesses
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
}
