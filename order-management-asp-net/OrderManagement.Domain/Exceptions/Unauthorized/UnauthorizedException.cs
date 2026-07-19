using OrderManagement.Domain.Enums.Custom;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Domain.Exceptions.Unauthorized;

public class UnauthorizedException : CustomException
{
    public UnauthorizedException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
