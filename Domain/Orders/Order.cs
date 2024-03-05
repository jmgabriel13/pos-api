using Domain.Customers;
using Domain.Events;
using Domain.Primitives;
using Domain.Products;

namespace Domain.Orders;

// rich domain model, write model
// aggregate, encapsulation
public sealed class Order : AggregateRoot<OrderId>
{
    private readonly List<LineItem> _lineItems = new();

    private Order(OrderId id, CustomerId customerId, OrderStatus status) : base(id)
    {
        CustomerId = customerId;
        Status = status;
    }

    private Order()
    {
    }

    public CustomerId CustomerId { get; private set; }
    public decimal Total
    {
        get
        {
            return _lineItems.Sum(item => item.Price.Amount);
        }
    }

    public OrderStatus Status { get; private set; }

    // expose a navigation property as readonly for ef core config,
    // so no one can access the original _lineItems by reference
    public IReadOnlyList<LineItem> LineItems => _lineItems.ToList();

    // static factory method approach to create a new order instance
    public static Order Create(CustomerId customerId)
    {
        var order = new Order(new OrderId(Guid.NewGuid()), customerId, OrderStatus.Pending);

        order.RaiseDomainEvent(new OrderCreatedDomainEvent(Guid.NewGuid(), order.Id));

        return order;
    }

    public void Add(Product product)
    {
        // we are catching a point in time snapshot of product price
        var lineItem = new LineItem(
            new LineItemId(Guid.NewGuid()),
            Id,
            product.Id,
            product.Price);

        _lineItems.Add(lineItem);
    }

    public void AddLineItem(ProductId productId, Money price)
    {
        var lineItem = new LineItem(new LineItemId(Guid.NewGuid()), Id, productId, price);

        _lineItems.Add(lineItem);
    }

    public void RemoveLineItem(LineItemId lineItemId)
    {
        var lineItem = _lineItems.FirstOrDefault(li => li.Id == lineItemId);

        if (lineItem is null)
        {
            return;
        }

        _lineItems.Remove(lineItem);
    }
}
