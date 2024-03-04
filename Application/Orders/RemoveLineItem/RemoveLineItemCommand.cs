using Application.Interfaces;
using Domain.Orders;

namespace Application.Orders.RemoveLineItem;
public sealed record RemoveLineItemCommand(OrderId orderId, LineItemId LineItemId) : ICommand;
