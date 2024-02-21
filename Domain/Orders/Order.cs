using Domain.Customers;
using Domain.Products;

namespace Domain.Orders;

// rich domain model
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

    // expose a navigation property as readonly for ef core config
    public IReadOnlyList<LineItem> LineItems => _lineItems.ToList();

    // factory method approach to create a new order instance
    public static Order Create(Customer cutomer)
    {
        var order = new Order
        {
            Id = new OrderId(Guid.NewGuid()),
            CustomerId = cutomer.Id,
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
