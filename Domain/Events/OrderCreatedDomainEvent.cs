using Domain.Orders;
using Domain.Primitives;

namespace Domain.Events;
public record OrderCreatedDomainEvent(Guid Id, OrderId OrderId) : DomainEvent(Id);
