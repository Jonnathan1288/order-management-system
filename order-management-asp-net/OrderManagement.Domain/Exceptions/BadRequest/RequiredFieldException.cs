using OrderManagement.Domain.Enums.Custom;

namespace OrderManagement.Domain.Exceptions.BadRequest;

public class RequiredFieldException : BadRequestException
{
    public RequiredFieldException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
