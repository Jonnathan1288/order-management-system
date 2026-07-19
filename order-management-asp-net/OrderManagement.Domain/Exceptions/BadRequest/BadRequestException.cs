using OrderManagement.Domain.Enums.Custom;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Domain.Exceptions.BadRequest;

public class BadRequestException : CustomException
{
    public BadRequestException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
