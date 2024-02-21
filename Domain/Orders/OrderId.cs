namespace Domain.Orders;

// strongly type id as record, immutability
public record OrderId(Guid Value);
