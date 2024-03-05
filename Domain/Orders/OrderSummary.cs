using Domain.Customers;

namespace Domain.Orders;

// read model
public record OrderSummary(OrderId Id, CustomerId CustomerId, decimal TotalPrice);
