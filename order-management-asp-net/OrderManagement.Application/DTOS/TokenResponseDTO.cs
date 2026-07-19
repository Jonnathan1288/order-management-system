
namespace OrderManagement.Application.DTOS;

public class TokenResponseDTO
{
    public string Token { get; set; } = string.Empty;
    public string? UserCode { get; set; }
    public string? DisplayName { get; set; }
    public string? PhotoUrl { get; set; }
    public bool IsManagement { get; set; }
}
