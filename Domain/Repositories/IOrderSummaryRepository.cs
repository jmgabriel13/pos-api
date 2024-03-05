using Domain.Orders;

namespace Domain.Repositories;
public interface IOrderSummaryRepository
{
    void Add(OrderSummary orderSummary);
    Task<OrderSummary?> GetSummaryByIdAsync(OrderId id, CancellationToken cancellationToken = default);
}
