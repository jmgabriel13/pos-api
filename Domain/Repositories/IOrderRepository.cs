using Domain.Orders;

namespace Domain.Repositories;
public interface IOrderRepository
{
    void Add(Order order);
    Task<Order?> GetByIdAsync(OrderId id, CancellationToken cancellationToken = default);
}
