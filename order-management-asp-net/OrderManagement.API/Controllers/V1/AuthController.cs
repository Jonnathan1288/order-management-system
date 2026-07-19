using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.Handlers;
using OrderManagement.Application.DTOS;
using OrderManagement.Application.Interfaces.Public;

namespace OrderManagement.API.Controllers.V1;

[Route("api/v1/auth")]
[ApiController]
public class AuthController(IUserService _service) : CommonController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDto)
    {
        return Ok(ResponseHandler.Ok(await _service.SignInAsync(loginRequestDto)));
    }
}