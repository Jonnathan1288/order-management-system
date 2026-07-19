
using OrderManagement.Application.DTOS;

namespace OrderManagement.Application.Interfaces.Public;

public interface IUserService
{
    public Task<TokenResponseDTO> SignInAsync(LoginRequestDTO model);
}
