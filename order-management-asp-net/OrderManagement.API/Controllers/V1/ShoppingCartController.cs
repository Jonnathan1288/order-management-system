using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.Handlers;
using OrderManagement.Application.Interfaces.Public;
using OrderManagement.Domain.Entities;

namespace OrderManagement.API.Controllers.V1;

[Route("api/v1/shopping-cart")]
[ApiController]
public class ShoppingCartController(IShoppingCartService _service) : CommonController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ShoppingCart shoppingCart)
    {
        return Ok(ResponseHandler.Ok(await _service.CreateAsync(BusinessId, CustomerId, shoppingCart)));
    }

    [HttpPatch("{shoppingCartId}/status/{status}")]
    public async Task<IActionResult> UpdateStatus(int shoppingCartId, string status)
    {
        return Ok(ResponseHandler.Ok(await _service.UpdateStatusAsync(BusinessId, CustomerId, shoppingCartId, status)));
    }
}
