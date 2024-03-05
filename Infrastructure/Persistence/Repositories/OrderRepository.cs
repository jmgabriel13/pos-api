using Domain.Orders;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public sealed class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Order order)
    {
        _context.Orders.Add(order);
    }

    public void AddOrderSummaries(OrderSummary orderSummary)
    {
        _context.OrderSummaries.Add(orderSummary);
    }

    public async Task<Order?> GetByIdAsync(OrderId id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public Task<OrderSummary?> GetSummaryByIdAsync(OrderId id, CancellationToken cancellationToken = default)
    {
        return _context.OrderSummaries
            .AsNoTracking()
            .FirstOrDefaultAsync(os => os.Id == id, cancellationToken);
    }

    public async Task<Order?> GetByOrderLineItemAsync(OrderId orderId, LineItemId lineItemId, CancellationToken cancellationToken = default)
    {
        var order = await _context
            .Orders
            .Include(o => o.LineItems.Where(li => li.Id == lineItemId))
            .SingleOrDefaultAsync(o => o.Id == orderId, cancellationToken);

        return order;
    }

}
