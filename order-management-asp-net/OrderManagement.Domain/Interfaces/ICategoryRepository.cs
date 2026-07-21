using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<Category> CreateAsync(Category category);

    Task<List<Category>> FindByBusinessAsync(short businessId);

    Task<List<Category>> FindByParentAsync(short businessId, int? parentId);
}
