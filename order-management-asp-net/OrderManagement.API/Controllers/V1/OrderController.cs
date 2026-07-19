using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.Handlers;
using OrderManagement.Application.Interfaces.Public;
using OrderManagement.Domain.Entities;

namespace OrderManagement.API.Controllers.V1;

[Route("api/v1/orders")]
[ApiController]
public class OrderController(IOrderService _service) : CommonController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Order order)
    {
        order.CustomerId = CustomerId;
        return Ok(ResponseHandler.Ok(await _service.CreateAsync(BusinessId, order)));
    }

    [HttpGet]
    public async Task<IActionResult> GetByBusiness()
    {
        return Ok(ResponseHandler.Ok(await _service.GetByBusinessAsync(BusinessId)));
    }

    [HttpGet("{orderId}/customer")]
    public async Task<IActionResult> GetByBusinessAndCustomer(int orderId)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByBusinessAndCustomerAsync(BusinessId, CustomerId, orderId)));
    }
}
