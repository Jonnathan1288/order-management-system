
namespace OrderManagement.Application.DTOS;

public class LoginRequestDTO
{
    public short BusinessId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

