using Application.Interfaces;
using Domain.Orders;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Orders.Commands.Create;
internal sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderSummaryRepository _orderSummaryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        IOrderSummaryRepository orderSummaryRepository)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _orderSummaryRepository = orderSummaryRepository;
    }

    public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = Order.Create(request.CustomerId);

        _orderRepository.Add(order);

        // this is a new order without line items thats why zero the total amount
        _orderSummaryRepository.Add(new OrderSummary(order.Id, request.CustomerId, 0));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
