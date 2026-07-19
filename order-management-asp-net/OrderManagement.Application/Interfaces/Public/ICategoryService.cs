using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Interfaces.Public;

public interface ICategoryService
{
    Task<List<Category>> GetByBusinessAsync(short businessId);

    Task<List<Category>> GetByParentAsync(short businessId, int? parentId);
}
