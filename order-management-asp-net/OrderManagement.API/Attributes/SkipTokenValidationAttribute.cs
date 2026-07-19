namespace OrderManagement.API.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public class SkipTokenValidationAttribute : Attribute
{
}
