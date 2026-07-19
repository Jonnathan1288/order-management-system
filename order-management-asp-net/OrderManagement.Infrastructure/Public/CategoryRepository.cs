using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Connections;

namespace OrderManagement.Infrastructure.Public;

public class CategoryRepository(PostgreSQLContext _context) : ICategoryRepository
{
    public Task<List<Category>> FindByBusinessAsync(short businessId)
    {
        return _context.Categories
            .AsNoTracking()
            .Where(category => category.BusinessId == businessId)
            .ToListAsync();
    }

    public Task<List<Category>> FindByParentAsync(short businessId, int? parentId)
    {
        return _context.Categories
            .AsNoTracking()
            .Where(category => category.BusinessId == businessId && category.ParentId == parentId)
            .ToListAsync();
    }
}
