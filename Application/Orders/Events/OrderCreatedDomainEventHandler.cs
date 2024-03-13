using Domain.Events;
using Domain.Orders;
using Domain.Repositories;
using MediatR;
using Rebus.Bus;

namespace Application.Orders.Events;
internal sealed class OrderCreatedDomainEventHandler : INotificationHandler<OrderCreatedDomainEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IBus _bus;

    public OrderCreatedDomainEventHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetByIdAsync(notification.OrderId, cancellationToken);

        if (order is null)
        {
            return;
        }
        // notification or email sender

        //_bus.Send(new OrderCreatedIntegrationEvent());
    }
}
