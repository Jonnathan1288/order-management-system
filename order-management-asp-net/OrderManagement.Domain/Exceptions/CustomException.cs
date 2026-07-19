
using OrderManagement.Domain.Enums.Custom;

namespace OrderManagement.Domain.Exceptions;

public class CustomException(ExceptionEnum exceptionEnum) : Exception
{
    private readonly ExceptionEnum _exceptionEnum = exceptionEnum;

    public string Code
    {
        get
        {
            return _exceptionEnum switch
            {
                _ => _exceptionEnum.ToString(),
            };
        }
    }
}
