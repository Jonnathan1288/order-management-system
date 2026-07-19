namespace OrderManagement.Domain.Entities;

public partial class Customer
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public short BusinessId { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Dni { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty ;

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual Business? Business { get; set; } 

    public virtual ICollection<Order> Orders { get; set; } = [];

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = [];

    public virtual User? User { get; set; } 
}
