using OrderManagement.Domain.Enums.Custom;

namespace OrderManagement.Domain.Exceptions.Conflict;

public class UniqueKeyException : ConflictException
{
    public UniqueKeyException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
