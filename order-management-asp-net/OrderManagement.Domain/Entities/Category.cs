namespace OrderManagement.Domain.Entities;

public partial class Category
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public short BusinessId { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual Business? Business { get; set; }

    public virtual ICollection<Category> InverseParent { get; set; } = [];

    public virtual Category? Parent { get; set; }

    public virtual ICollection<Product> Products { get; set; } = [];
}
