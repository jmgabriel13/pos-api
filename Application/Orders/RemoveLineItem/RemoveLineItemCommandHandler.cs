using Application.Interfaces;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Orders.RemoveLineItem;
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
        var order = await _orderRepository.GetByOrderLineItemAsync(request.orderId, request.LineItemId, cancellationToken);

        if (order == null)
        {
            return Result.FirstFailureOrSuccess();
        }

        order.RemoveLineItem(request.LineItemId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
