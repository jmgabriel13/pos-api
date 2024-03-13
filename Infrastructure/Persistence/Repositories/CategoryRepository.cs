using Domain.Categories;
using Domain.Repositories;

namespace Infrastructure.Persistence.Repositories;
public sealed class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Category category)
    {
        _context.Add(category);
    }
}
