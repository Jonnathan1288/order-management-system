using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.Handlers;
using OrderManagement.Application.Interfaces.Public;

namespace OrderManagement.API.Controllers.V1;

[Route("api/v1/categories")]
[ApiController]
public class CategoryController(ICategoryService _service) : CommonController
{
    [HttpGet]
    public async Task<IActionResult> GetByBusiness()
    {
        return Ok(ResponseHandler.Ok(await _service.GetByBusinessAsync(BusinessId)));
    }

    [HttpGet("parent")]
    public async Task<IActionResult> GetByParent([FromQuery] int? parentId)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByParentAsync(BusinessId, parentId)));
    }
}
