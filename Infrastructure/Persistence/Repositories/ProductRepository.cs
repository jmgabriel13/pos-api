using Domain.Products;
using Domain.Repositories;

namespace Infrastructure.Persistence.Repositories;
public sealed class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
    }
}
