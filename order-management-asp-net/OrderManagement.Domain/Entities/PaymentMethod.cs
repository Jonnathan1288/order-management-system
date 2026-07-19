using System.Text.Json.Serialization;

namespace OrderManagement.Domain.Entities;

public partial class PaymentMethod
{
    public short Id { get; set; }

    public short BusinessId { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual Business? Business { get; set; }

    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = [];
}
