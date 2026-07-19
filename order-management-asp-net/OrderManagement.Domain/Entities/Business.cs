
namespace OrderManagement.Domain.Entities;

public partial class Business
{
    public short Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty    ;

    public string Address { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string LogoUrl { get; set; } = string.Empty;

    public string Template { get; set; } = string.Empty;

    public bool UseTemplate { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = [];

    public virtual ICollection<Customer> Customers { get; set; } = [];

    public virtual ICollection<Order> Orders { get; set; } = [] ;

    public virtual ICollection<PaymentMethod> PaymentMethods { get; set; } = [];

    public virtual ICollection<Product> Products { get; set; } = [];

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = [];

    public virtual ICollection<User> Users { get; set; } = [];
}
