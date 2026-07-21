using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Connections;

namespace OrderManagement.Infrastructure.Public;

public class ProductRepository(PostgreSQLContext _context) : IProductRepository
{
    public async Task<Product> CreateAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public Task<List<Product>> FindByBusinessAsync(short businessId)
    {
        return _context.Products
            .AsNoTracking()
            .Where(product => product.BusinessId == businessId)
            .ToListAsync();
    }

    public Task<List<Product>> FindByCategoryAsync(short businessId, int categoryId)
    {
        return _context.Products
            .AsNoTracking()
            .Where(product => product.BusinessId == businessId && product.CategoryId == categoryId)
            .ToListAsync();
    }

    public Task<List<Product>> SearchAsync(short businessId, string search)
    {
        string value = search.ToLower();
        return _context.Products
            .AsNoTracking()
            .Where(product => product.BusinessId == businessId
                && (product.Name.ToLower().Contains(value)
                    || product.Description.ToLower().Contains(value)
                    || product.Brand.ToLower().Contains(value)))
            .ToListAsync();
    }
}
