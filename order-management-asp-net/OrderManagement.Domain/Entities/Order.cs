using System.Text.Json.Serialization;

namespace OrderManagement.Domain.Entities;
public partial class Order
{
    public int Id { get; set; }

    public short BusinessId { get; set; }

    public int CustomerId { get; set; }

    public short PaymentMethodId { get; set; }

    /// <summary>
    /// ACTIVE
    /// CANCELLED
    /// COMPLETED
    /// </summary>
    public string Status { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public decimal Total { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Tax { get; set; }

    public decimal Discount { get; set; }

    public string DeliveryAddress { get; set; } = string.Empty;

    public string? Notes { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual Business? Business { get; set; } 

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = [];

    public virtual PaymentMethod? PaymentMethod { get; set; } 
}
