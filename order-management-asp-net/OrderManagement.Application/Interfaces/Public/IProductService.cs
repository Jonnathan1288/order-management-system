using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Interfaces.Public;

public interface IProductService
{
    Task<Product> CreateAsync(short businessId, Product product);

    Task<List<Product>> GetByBusinessAsync(short businessId);

    Task<List<Product>> GetByCategoryAsync(short businessId, int categoryId);

    Task<List<Product>> SearchAsync(short businessId, string search);
}
