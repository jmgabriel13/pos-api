using Domain.Customers;

namespace Domain.Orders;

// read model
public record OrderSummary(Guid Id, CustomerId CustomerId, decimal TotalPrice);
