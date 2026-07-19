namespace OrderManagement.Domain.Enums.Custom;

public enum ExceptionEnum : short
{
    ExpiredToken,
    InvalidToken,
    UserDisabled,
    UserNotFound,
    WrongPassword,
}
