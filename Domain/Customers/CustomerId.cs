namespace Domain.Customers;

// strongly type id as record, immutability
public record CustomerId(Guid Value);