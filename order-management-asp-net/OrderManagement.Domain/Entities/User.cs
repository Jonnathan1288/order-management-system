using System.Text.Json.Serialization;

namespace OrderManagement.Domain.Entities;

public partial class User
{
    public int Id { get; set; }

    public short BusinessId { get; set; }

    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// ADMIN
    /// CUSTOMER
    /// </summary>
    public string Role { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual Business? Business { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = [];
}
