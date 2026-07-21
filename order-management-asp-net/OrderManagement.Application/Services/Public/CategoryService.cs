using OrderManagement.Application.Interfaces.Public;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Services.Public;

public class CategoryService(ICategoryRepository _repository) : ICategoryService
{
    public Task<Category> CreateAsync(short businessId, Category category)
    {
        category.BusinessId = businessId;
        return _repository.CreateAsync(category);
    }

    public Task<List<Category>> GetByBusinessAsync(short businessId)
    {
        return _repository.FindByBusinessAsync(businessId);
    }

    public Task<List<Category>> GetByParentAsync(short businessId, int? parentId)
    {
        return _repository.FindByParentAsync(businessId, parentId);
    }
}
