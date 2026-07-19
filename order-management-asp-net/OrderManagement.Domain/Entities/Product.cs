namespace OrderManagement.Domain.Entities;

public partial class Product
{
    public int Id { get; set; }

    public short BusinessId { get; set; }

    public int CategoryId { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Brand { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal? Tax { get; set; }

    public decimal? Discount { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual Business? Business { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = [];
}
