using Application.Interfaces;
using Domain.Orders;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Orders.Commands.Create;
internal sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = Order.Create(request.CustomerId);

        _orderRepository.Add(order);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
