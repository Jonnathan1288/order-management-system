using OrderManagement.Application.Interfaces.Public;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Services.Public;

public class ProductService(IProductRepository _repository) : IProductService
{
    public Task<Product> CreateAsync(short businessId, Product product)
    {
        product.BusinessId = businessId;
        return _repository.CreateAsync(product);
    }

    public Task<List<Product>> GetByBusinessAsync(short businessId)
    {
        return _repository.FindByBusinessAsync(businessId);
    }

    public Task<List<Product>> GetByCategoryAsync(short businessId, int categoryId)
    {
        return _repository.FindByCategoryAsync(businessId, categoryId);
    }

    public Task<List<Product>> SearchAsync(short businessId, string search)
    {
        return _repository.SearchAsync(businessId, search);
    }
}
