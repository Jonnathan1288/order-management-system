using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> FindByBusinessAsync(short businessId);

    Task<List<Product>> FindByCategoryAsync(short businessId, int categoryId);

    Task<List<Product>> SearchAsync(short businessId, string search);
}
