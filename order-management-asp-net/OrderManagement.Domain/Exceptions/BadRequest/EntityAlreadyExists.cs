using OrderManagement.Domain.Enums.Custom;

namespace OrderManagement.Domain.Exceptions.BadRequest;

public class EntityAlreadyExists : BadRequestException
{
    public EntityAlreadyExists(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
