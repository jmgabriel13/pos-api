using Domain.Orders;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public sealed class OrderSummaryRepository : IOrderSummaryRepository
{
    private readonly ApplicationDbContext _context;

    public OrderSummaryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(OrderSummary orderSummary)
    {
        _context.OrderSummaries.Add(orderSummary);
    }

    public Task<OrderSummary?> GetSummaryByIdAsync(OrderId id, CancellationToken cancellationToken = default)
    {
        return _context.OrderSummaries
            .AsNoTracking()
            .FirstOrDefaultAsync(os => os.Id == id, cancellationToken);
    }
}
