
using OrderManagement.Application.DTOS;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Interfaces.Public;

public interface IUserService
{
    public Task<TokenResponseDTO> SignInAsync(LoginRequestDTO model);
    public Task<User> VerifyToken(string userToken);
}
