using Domain.Events;
using Domain.Repositories;
using MediatR;

namespace Application.Orders.Events;
internal sealed class OrderCreatedDomainEventHandler : INotificationHandler<OrderCreatedDomainEvent>
{
    private readonly IOrderRepository _orderRepository;

    public OrderCreatedDomainEventHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        // notification or email sender
        throw new NotImplementedException();
    }
}
