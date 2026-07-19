using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.Handlers;
using OrderManagement.Application.Interfaces.Public;

namespace OrderManagement.API.Controllers.V1;

[Route("api/v1/payment-methods")]
[ApiController]
public class PaymentMethodController(IPaymentMethodService _service) : CommonController
{
    [HttpGet]
    public async Task<IActionResult> GetByBusiness()
    {
        return Ok(ResponseHandler.Ok(await _service.GetByBusinessAsync(BusinessId)));
    }
}
