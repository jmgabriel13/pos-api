using Domain.Customers;
using Domain.Products;

namespace Domain.Orders;

// rich domain model, write model
// aggregate, encapsulation
public class Order
{
    private readonly List<LineItem> _lineItems = new();
    private Order()
    {
    }
    public OrderId Id { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }

    // expose a navigation property as readonly for ef core config,
    // so no one can access the original _lineItems by reference
    public IReadOnlyList<LineItem> LineItems => _lineItems.ToList();

    // static factory method approach to create a new order instance
    public static Order Create(CustomerId customerId)
    {
        var order = new Order
        {
            Id = new OrderId(Guid.NewGuid()),
            CustomerId = customerId,
            Status = OrderStatus.Pending
        };

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
}
