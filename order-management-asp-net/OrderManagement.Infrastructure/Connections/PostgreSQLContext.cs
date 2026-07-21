
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OrderManagement.Domain.Entities;
using System.Text;
using System.Text.Json;

namespace OrderManagement.Infrastructure.Connections;

public partial class PostgreSQLContext : DbContext
{
    public PostgreSQLContext()
    {
    }

    public PostgreSQLContext(DbContextOptions<PostgreSQLContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Business> Businesses { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    // <summary>
    /// Generate random code.
    /// </summary>
    /// <param name="length">The code length.</param>
    /// <returns>A string code.</returns>
    private static string GenerateCode(short length = 20)
    {
        char[] _chars =
        [
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
                'k', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
                'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
                'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'o',
                'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y',
                'z', '0', '1', '2', '3', '4', '5', '6', '7', '8',
                '9'
        ];
        StringBuilder sb = new();
        Random random = new();
        for (int i = 0; i < length; i++)
        {
            sb.Append(_chars[random.Next(0, _chars.Length)]);
        }
        return sb.ToString();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<EntityEntry> entries = ChangeTracker.Entries().Where(entry =>
        {
            return entry.State == EntityState.Added || entry.State == EntityState.Modified;
        });
        foreach (EntityEntry entry in entries)
        {
            // Always change update date.
            if (entry.Entity.GetType().GetProperty("UpdateDate") != null) Entry(entry.Entity).Property("UpdateDate").CurrentValue = DateTime.UtcNow;
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity.GetType().GetProperty("Code") != null) Entry(entry.Entity).Property("Code").CurrentValue = GenerateCode();
                if (entry.Entity.GetType().GetProperty("Active") != null) Entry(entry.Entity).Property("Active").CurrentValue = true;
                if (entry.Entity.GetType().GetProperty("CreationDate") != null) Entry(entry.Entity).Property("CreationDate").CurrentValue = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                if (entry.Entity.GetType().GetProperty("CreationDate") != null) Entry(entry.Entity).Property("CreationDate").IsModified = false;
                if (entry.Entity.GetType().GetProperty("Code") != null) Entry(entry.Entity).Property("Code").IsModified = false;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Business>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("businesses_pkey");

            entity.ToTable("businesses");

            entity.HasIndex(e => e.Code, "businesses_code_key").IsUnique();

            entity.HasIndex(e => e.Name, "businesses_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.Country)
                .HasMaxLength(70)
                .HasColumnName("country");
            entity.Property(e => e.CreationDate).HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.LogoUrl)
                .HasMaxLength(255)
                .HasColumnName("logo_url");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Template).HasColumnName("template");
            entity.Property(e => e.UpdateDate).HasColumnName("update_date");
            entity.Property(e => e.UseTemplate).HasColumnName("use_template");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.HasIndex(e => new { e.BusinessId, e.Name }, "categories_business_id_name_key").IsUnique();

            entity.HasIndex(e => e.Code, "categories_code_key").IsUnique();

            entity.HasIndex(e => e.ParentId, "categories_parent_id_idx");

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate).HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.UpdateDate).HasColumnName("update_date");

            entity.HasOne(d => d.Business).WithMany(p => p.Categories)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_categories__business_id");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("fk_categories__parent_id");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.HasIndex(e => new { e.BusinessId, e.Dni }, "customers_business_id_dni_key").IsUnique();

            entity.HasIndex(e => e.Code, "customers_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate).HasColumnName("creation_date");
            entity.Property(e => e.Dni)
                .HasMaxLength(13)
                .HasColumnName("dni");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.UpdateDate).HasColumnName("update_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Business).WithMany(p => p.Customers)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customers__business_id");

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customers__user_id");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.HasIndex(e => e.BusinessId, "orders_business_id_idx");

            entity.HasIndex(e => e.CustomerId, "orders_customer_id_idx");

            entity.HasIndex(e => e.PaymentMethodId, "orders_payment_method_id_idx");

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.CreationDate).HasColumnName("creation_date");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(255)
                .HasColumnName("delivery_address");
            entity.Property(e => e.Discount)
                .HasPrecision(8, 2)
                .HasColumnName("discount");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasComment("ACTIVE\r\nCANCELLED\r\nCOMPLETED")
                .HasColumnName("status");
            entity.Property(e => e.SubTotal)
                .HasPrecision(8, 2)
                .HasColumnName("sub_total");
            entity.Property(e => e.Tax)
                .HasPrecision(8, 2)
                .HasColumnName("tax");
            entity.Property(e => e.Total)
                .HasPrecision(8, 2)
                .HasColumnName("total");
            entity.Property(e => e.UpdateDate).HasColumnName("update_date");

            entity.HasOne(d => d.Business).WithMany(p => p.Orders)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orders__business_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orders__customer_id");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orders__payment_method_id");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_details_pkey");

            entity.ToTable("order_details");

            entity.HasIndex(e => new { e.OrderId, e.ProductId }, "order_details_order_id_product_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CreationDate).HasColumnName("creation_date");
            entity.Property(e => e.Discount)
                .HasPrecision(8, 2)
                .HasColumnName("discount");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SubTotal)
                .HasPrecision(8, 2)
                .HasColumnName("sub_total");
            entity.Property(e => e.Tax)
                .HasPrecision(8, 2)
                .HasColumnName("tax");
            entity.Property(e => e.Total)
                .HasPrecision(8, 2)
                .HasColumnName("total");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(8, 2)
                .HasColumnName("unit_price");
            entity.Property(e => e.UpdateDate).HasColumnName("update_date");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_details__order_id");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_details__product_id");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payment_methods_pkey");

            entity.ToTable("payment_methods");

            entity.HasIndex(e => e.BusinessId, "payment_methods_business_id_idx");

            entity.HasIndex(e => new { e.BusinessId, e.Name }, "payment_methods_business_id_name_key").IsUnique();

            entity.HasIndex(e => e.Code, "payment_methods_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate).HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate).HasColumnName("update_date");

            entity.HasOne(d => d.Business).WithMany(p => p.PaymentMethods)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_payment_methods__business_id");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("products_pkey");

            entity.ToTable("products");

            entity.HasIndex(e => new { e.BusinessId, e.CategoryId, e.Name }, "products_business_id_category_id_name_key").IsUnique();

            entity.HasIndex(e => e.Code, "products_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Brand)
                .HasMaxLength(150)
                .HasColumnName("brand");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate).HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Discount)
                .HasPrecision(8, 2)
                .HasColumnName("discount");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(8, 2)
                .HasColumnName("price");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.Tax)
                .HasPrecision(8, 2)
                .HasColumnName("tax");
            entity.Property(e => e.UpdateDate).HasColumnName("update_date");

            entity.HasOne(d => d.Business).WithMany(p => p.Products)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_products__business_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_products__category_id");
        });

        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shopping_carts_pkey");

            entity.ToTable("shopping_carts");

            entity.HasIndex(e => e.BusinessId, "shopping_carts_business_id_idx");

            entity.HasIndex(e => e.CustomerId, "shopping_carts_customer_id_idx");

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.Code)
                .HasColumnType("character varying")
                .HasColumnName("code");
            entity.Property(e => e.CreationDate).HasColumnName("creation_date");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Payload)
                .HasConversion(
                    payload => JsonSerializer.Serialize(payload, (JsonSerializerOptions?)null),
                    payload => JsonSerializer.Deserialize<Dictionary<string, object>>(payload, (JsonSerializerOptions?)null) ?? new Dictionary<string, object>())
                .HasColumnType("jsonb")
                .HasColumnName("payload");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasComment("ACTIVE\r\nCANCELLED\r\nCOMPLETED")
                .HasColumnName("status");
            entity.Property(e => e.UpdateDate).HasColumnName("update_date");

            entity.HasOne(d => d.Business).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shopping_carts__business_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shopping_carts__customer_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => new { e.BusinessId, e.Email }, "users_business_id_email_key").IsUnique();

            entity.HasIndex(e => e.Code, "users_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, null, true, null)
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate).HasColumnName("creation_date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasComment("ADMIN\r\nCUSTOMER")
                .HasColumnName("role");
            entity.Property(e => e.UpdateDate).HasColumnName("update_date");

            entity.HasOne(d => d.Business).WithMany(p => p.Users)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_users__business_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
