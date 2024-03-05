using Domain.Orders;

namespace Domain.Repositories;
public interface IOrderRepository
{
    void Add(Order order);
    void AddOrderSummaries(OrderSummary orderSummary);
    Task<Order?> GetByIdAsync(OrderId id, CancellationToken cancellationToken = default);
    Task<OrderSummary?> GetSummaryByIdAsync(OrderId id, CancellationToken cancellationToken = default);
    Task<Order?> GetByOrderLineItemAsync(OrderId orderId, LineItemId lineItemId, CancellationToken cancellationToken = default);
}
