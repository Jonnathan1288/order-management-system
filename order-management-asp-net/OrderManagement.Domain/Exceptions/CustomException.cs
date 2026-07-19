
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
                ExceptionEnum.ExpiredToken => "expired-token",
                ExceptionEnum.InvalidToken => "invalid-token",
                ExceptionEnum.UserDisabled => "user-disabled",
                ExceptionEnum.UserNotFound => "user-not-found",
                ExceptionEnum.WrongPassword => "wrong-password",
                _ => _exceptionEnum.ToString(),
            };
        }
    }
}

