using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.Handlers;
using OrderManagement.Application.Interfaces.Public;

namespace OrderManagement.API.Controllers.V1;

[Route("api/v1/products")]
[ApiController]
public class ProductController(IProductService _service) : CommonController
{
    [HttpGet]
    public async Task<IActionResult> GetByBusiness()
    {
        return Ok(ResponseHandler.Ok(await _service.GetByBusinessAsync(BusinessId)));
    }

    [HttpGet("categories/{categoryId}")]
    public async Task<IActionResult> GetByCategory(int categoryId)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByCategoryAsync(BusinessId, categoryId)));
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string value)
    {
        return Ok(ResponseHandler.Ok(await _service.SearchAsync(BusinessId, value)));
    }
}
