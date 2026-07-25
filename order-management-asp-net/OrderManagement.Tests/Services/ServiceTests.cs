using OrderManagement.Application.Services.Public;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Tests.Services;

public class ServiceTests
{
    [Fact]
    public async Task ProductCreateAsync_AssignsBusinessId()
    {
        InMemoryProductRepository repository = new();
        ProductService service = new(repository);
        Product product = new()
        {
            CategoryId = 3,
            Name = "Pan integral",
            Description = "Pan artesanal de masa madre",
            Brand = "Panaderia Central",
            Price = 1.50m,
            Stock = 30,
        };

        Product result = await service.CreateAsync(7, product);

        Assert.Equal(7, result.BusinessId);
        Assert.Single(repository.Products);
    }

    [Fact]
    public async Task CategoryCreateAsync_AssignsBusinessId()
    {
        InMemoryCategoryRepository repository = new();
        CategoryService service = new(repository);
        Category category = new()
        {
            Name = "Pan",
            Description = "Pan continental",
        };

        Category result = await service.CreateAsync(4, category);

        Assert.Equal(4, result.BusinessId);
        Assert.Single(repository.Categories);
    }

    [Fact]
    public async Task ProductSearchAsync_SearchesNameDescriptionAndBrand()
    {
        InMemoryProductRepository repository = new();
        repository.Products.AddRange(
        [
            new() { BusinessId = 1, Name = "Croissant", Description = "Hojaldre con mantequilla", Brand = "Panaderia Central" },
            new() { BusinessId = 1, Name = "Baguette", Description = "Pan frances artesanal", Brand = "Horno del Sur" },
            new() { BusinessId = 2, Name = "Croissant", Description = "Producto de otra empresa", Brand = "Panaderia Central" },
        ]);
        ProductService service = new(repository);

        List<Product> results = await service.SearchAsync(1, "mantequilla");

        Product result = Assert.Single(results);
        Assert.Equal("Croissant", result.Name);
        Assert.Equal((short)1, result.BusinessId);
    }

    [Fact]
    public async Task ShoppingCartCreateAsync_CancelsPreviousActiveCartAndCreatesActiveCart()
    {
        InMemoryShoppingCartRepository repository = new();
        repository.ShoppingCarts.Add(new()
        {
            Id = 1,
            BusinessId = 5,
            CustomerId = 9,
            Status = "ACTIVE",
            Payload = new() { ["productId"] = 1 },
        });
        ShoppingCartService service = new(repository);
        ShoppingCart shoppingCart = new()
        {
            Payload = new() { ["productId"] = 2 },
        };

        ShoppingCart result = await service.CreateAsync(5, 9, shoppingCart);

        Assert.Equal("CANCELLED", repository.ShoppingCarts[0].Status);
        Assert.Equal("ACTIVE", result.Status);
        Assert.Equal(5, result.BusinessId);
        Assert.Equal(9, result.CustomerId);
        Assert.Equal(2, repository.ShoppingCarts.Count);
    }

    private class InMemoryProductRepository : IProductRepository
    {
        public List<Product> Products { get; } = [];

        public Task<Product> CreateAsync(Product product)
        {
            product.Id = Products.Count + 1;
            Products.Add(product);
            return Task.FromResult(product);
        }

        public Task<List<Product>> FindByBusinessAsync(short businessId)
        {
            return Task.FromResult(Products.Where(product => product.BusinessId == businessId).ToList());
        }

        public Task<List<Product>> FindByCategoryAsync(short businessId, int categoryId)
        {
            return Task.FromResult(Products
                .Where(product => product.BusinessId == businessId && product.CategoryId == categoryId)
                .ToList());
        }

        public Task<List<Product>> SearchAsync(short businessId, string search)
        {
            string value = search.ToLower();
            return Task.FromResult(Products
                .Where(product => product.BusinessId == businessId
                    && (product.Name.ToLower().Contains(value)
                        || product.Description.ToLower().Contains(value)
                        || product.Brand.ToLower().Contains(value)))
                .ToList());
        }
    }

    private class InMemoryCategoryRepository : ICategoryRepository
    {
        public List<Category> Categories { get; } = [];

        public Task<Category> CreateAsync(Category category)
        {
            category.Id = Categories.Count + 1;
            Categories.Add(category);
            return Task.FromResult(category);
        }

        public Task<List<Category>> FindByBusinessAsync(short businessId)
        {
            return Task.FromResult(Categories.Where(category => category.BusinessId == businessId).ToList());
        }

        public Task<List<Category>> FindByParentAsync(short businessId, int? parentId)
        {
            return Task.FromResult(Categories
                .Where(category => category.BusinessId == businessId && category.ParentId == parentId)
                .ToList());
        }
    }

    private class InMemoryShoppingCartRepository : IShoppingCartRepository
    {
        public List<ShoppingCart> ShoppingCarts { get; } = [];

        public Task<ShoppingCart> CreateAsync(ShoppingCart shoppingCart)
        {
            shoppingCart.Id = ShoppingCarts.Count + 1;
            ShoppingCarts.Add(shoppingCart);
            return Task.FromResult(shoppingCart);
        }

        public Task<ShoppingCart?> FindAsync(short businessId, int customerId, int shoppingCartId)
        {
            return Task.FromResult(ShoppingCarts.FirstOrDefault(shoppingCart => shoppingCart.BusinessId == businessId
                && shoppingCart.CustomerId == customerId
                && shoppingCart.Id == shoppingCartId));
        }

        public Task CancelActiveAsync(short businessId, int customerId, int? exceptShoppingCartId = null)
        {
            foreach (ShoppingCart shoppingCart in ShoppingCarts.Where(shoppingCart => shoppingCart.BusinessId == businessId
                && shoppingCart.CustomerId == customerId
                && shoppingCart.Status == "ACTIVE"
                && (!exceptShoppingCartId.HasValue || shoppingCart.Id != exceptShoppingCartId.Value)))
            {
                shoppingCart.Status = "CANCELLED";
            }

            return Task.CompletedTask;
        }

        public Task<ShoppingCart> UpdateAsync(ShoppingCart shoppingCart)
        {
            return Task.FromResult(shoppingCart);
        }
    }
}
