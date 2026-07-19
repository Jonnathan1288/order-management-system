using OrderManagement.Domain.Entities;
using OrderManagement.Application.DTOS;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Domain.Exceptions.Unauthorized;
using OrderManagement.Domain.Enums.Custom;
using OrderManagement.Application.Interfaces.Custom;
using OrderManagement.Application.Interfaces.Public;

namespace OrderManagement.Application.Services.Public;

public class UserService(
    IUserRepository _repository,
    IBusinessRepository _buseinesRepository,
    IJWTService _jwtService) : IUserService
{
    public async Task<TokenResponseDTO> SignInAsync(LoginRequestDTO model)
    {
        Business business = await _buseinesRepository.FindFirstAsync() ?? throw new BadCredentialException(ExceptionEnum.UserNotFound); 
        User? user = await _repository.FindByBussinesAndEmailAsync(business.Id, model.Email.Trim()) ?? throw new BadCredentialException(ExceptionEnum.UserNotFound);

        if (!user.Active) throw new AccountException(ExceptionEnum.UserDisabled);

        if (!BCrypt.BCrypt.CheckPassword(model.Password, user.Password ?? string.Empty)) throw new BadCredentialException(ExceptionEnum.WrongPassword);

        return new()
        {
            Token = _jwtService.GenerateToken(user, 3600),
        };
    }

    public async Task<User> VerifyToken(string userToken)
    {
        int userId = _jwtService.GetUserId(userToken);
        User user = await _repository.FindByIdAsync(userId) ?? throw new UnauthorizedException(ExceptionEnum.InvalidToken);
        return user;
    }
}