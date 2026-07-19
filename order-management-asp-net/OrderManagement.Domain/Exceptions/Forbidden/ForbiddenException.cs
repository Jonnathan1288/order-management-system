using OrderManagement.Domain.Enums.Custom;

namespace OrderManagement.Domain.Exceptions.Forbidden;

public class ForbiddenException : CustomException
{
    public ForbiddenException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
