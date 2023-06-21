using Domain.Products;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

internal sealed class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Product?> GetByIdAsync(ProductId id)
    {
        return _context.Products
                    .SingleOrDefaultAsync(p => p.Id == id);
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
    }
    public void Update(Product product)
    {
        _context.Products.Update(product);
    }
    public void Remove(Product product)
    {
        _context.Products.Remove(product);
    }
}