﻿using Domain.Products;

namespace Domain.Orders;

public class LineItem
{
    public LineItem(LineItemId id, OrderId orderId, ProductId productId, Money price)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        Price = price;
    }

    private LineItem()
    {
    }

    public LineItemId Id { get; private set; }
    public OrderId OrderId { get; private set; }
    public ProductId ProductId { get; private set; }

    // we use this price for detachment to product entities,
    // because the price of product is changeable
    public Money Price { get; private set; }
}
