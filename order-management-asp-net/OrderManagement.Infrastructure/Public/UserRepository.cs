using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Connections;

namespace OrderManagement.Infrastructure.Public;

public class UserRepository(PostgreSQLContext _context) : IUserRepository
{
    public Task<User?> FindByBussinesAndEmailAsync(short businessId, string email)
    {
        return _context.Users
            .AsNoTracking()
            .Include(u => u.Customers)
            .FirstOrDefaultAsync(u => u.BusinessId == businessId && u.Email.Equals(email));
    }

    public Task<User?> FindByIdAsync(int id) 
    {
        return _context.Users
            .AsNoTracking()
            .Include(u => u.Customers)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

}
