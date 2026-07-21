using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.Attributes;
using OrderManagement.API.Handlers;
using OrderManagement.Application.Interfaces.Public;

namespace OrderManagement.API.Controllers.V1;

[Route("api/v1/business")]
[ApiController]
public class BusinessController(IBusinessService _service) : CommonController
{
    [HttpGet]
    [SkipTokenValidation]
    public async Task<IActionResult> GetByBusiness()
    {
        return Ok(ResponseHandler.Ok(await _service.GetFirstAsync()));
    }
}