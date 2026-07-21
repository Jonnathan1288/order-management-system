using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.Attributes;
using OrderManagement.API.Handlers;
using OrderManagement.Application.Interfaces.Public;
using OrderManagement.Domain.Entities;

namespace OrderManagement.API.Controllers.V1;

[Route("api/v1/product")]
[ApiController]
public class ProductController(IProductService _service) : CommonController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
        return Ok(ResponseHandler.Ok(await _service.CreateAsync(BusinessId, product)));
    }

    [HttpGet]
    [SkipTokenValidation]
    public async Task<IActionResult> GetByBusiness()
    {
        return Ok(ResponseHandler.Ok(await _service.GetByBusinessAsync(BusinessId)));
    }

    [HttpGet("categories/{categoryId}")]
    [SkipTokenValidation]
    public async Task<IActionResult> GetByCategory(int categoryId)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByCategoryAsync(BusinessId, categoryId)));
    }

    [HttpGet("search")]
    [SkipTokenValidation]
    public async Task<IActionResult> Search([FromQuery] string value)
    {
        return Ok(ResponseHandler.Ok(await _service.SearchAsync(BusinessId, value)));
    }
}
