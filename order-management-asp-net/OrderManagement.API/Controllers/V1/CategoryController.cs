using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.Attributes;
using OrderManagement.API.Handlers;
using OrderManagement.Application.Interfaces.Public;
using OrderManagement.Domain.Entities;

namespace OrderManagement.API.Controllers.V1;

[Route("api/v1/category")]
[ApiController]
public class CategoryController(ICategoryService _service) : CommonController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Category category)
    {
        return Ok(ResponseHandler.Ok(await _service.CreateAsync(BusinessId, category)));
    }

    [HttpGet]
    [SkipTokenValidation]
    public async Task<IActionResult> GetByBusiness()
    {
        return Ok(ResponseHandler.Ok(await _service.GetByBusinessAsync(BusinessId)));
    }

    [HttpGet("parent")]
    [SkipTokenValidation]
    public async Task<IActionResult> GetByParent([FromQuery] int? parentId)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByParentAsync(BusinessId, parentId)));
    }
}
