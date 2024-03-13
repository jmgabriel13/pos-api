using Domain.Orders;
using Domain.Primitives;

namespace Domain.Events;
public record LineItemRemovedDomainEvent(Guid Id, OrderId Order, LineItemId LineItemId) : DomainEvent(Id);
