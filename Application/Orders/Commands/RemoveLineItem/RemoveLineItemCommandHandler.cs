using Application.Interfaces;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Orders.Commands.RemoveLineItem;
internal sealed class RemoveLineItemCommandHandler : ICommandHandler<RemoveLineItemCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveLineItemCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveLineItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository
            .GetByIdWithLineItemAsync(request.OrderId, request.LineItemId, cancellationToken);

        if (order is null)
        {
            return Result.Failure(new Error("Order.NotFound", "Order not found"));
        }

        order.RemoveLineItem(request.LineItemId, _orderRepository);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
