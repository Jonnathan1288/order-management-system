
using OrderManagement.Domain.Enums.Custom;

namespace OrderManagement.Domain.Exceptions.Unauthorized;

public class AccountException : UnauthorizedException
{
    public AccountException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
