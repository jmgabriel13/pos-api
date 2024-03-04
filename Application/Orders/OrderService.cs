using Application.Interfaces;
using Domain.Customers;
using Domain.Orders;
using Domain.Products;
using Domain.Repositories;

namespace Application.Orders;
public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Create(CustomerId customerId, CancellationToken cancellationToken = default)
    {
        var order = Order.Create(customerId);

        _orderRepository.Add(order);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task AddLineItem(OrderId orderId, Product product, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);

        order?.AddLineItem(product.Id, product.Price);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

    }
}
