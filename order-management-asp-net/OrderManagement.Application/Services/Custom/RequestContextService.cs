using OrderManagement.Application.Interfaces.Custom;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Services.Custom;

public class RequestContextService : IRequestContextService
{
    public User? User { get; set; }
}


