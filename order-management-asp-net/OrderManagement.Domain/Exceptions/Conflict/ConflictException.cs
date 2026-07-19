using OrderManagement.Domain.Enums.Custom;

namespace OrderManagement.Domain.Exceptions.Conflict;

public class ConflictException : CustomException
{
    public ConflictException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
