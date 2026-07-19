using OrderManagement.Domain.Enums.Custom;

namespace OrderManagement.Domain.Exceptions.BadRequest;

public class InvalidFieldException : BadRequestException
{
    public InvalidFieldException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
