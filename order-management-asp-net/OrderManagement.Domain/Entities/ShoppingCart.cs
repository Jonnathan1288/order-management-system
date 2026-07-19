namespace OrderManagement.Domain.Entities;
public partial class ShoppingCart
{
    public int Id { get; set; }

    public short BusinessId { get; set; }

    public int CustomerId { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Payload { get; set; } = string.Empty;

    /// <summary>
    /// ACTIVE
    /// CANCELLED
    /// COMPLETED
    /// </summary>
    public string Status { get; set; } = string.Empty;

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual Business? Business { get; set; } 

    public virtual Customer? Customer { get; set; }
}
