using OrderManagement.Domain.Enums.Custom;

namespace OrderManagement.Domain.Exceptions.Unauthorized;

public class BadCredentialException : UnauthorizedException
{
    public BadCredentialException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
