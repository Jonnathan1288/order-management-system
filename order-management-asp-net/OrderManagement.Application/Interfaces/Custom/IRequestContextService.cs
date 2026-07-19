using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Interfaces.Custom;

public interface IRequestContextService
{
    public User? User { get; set; }
}
