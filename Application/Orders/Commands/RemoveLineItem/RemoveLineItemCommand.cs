using Application.Interfaces;
using Domain.Orders;

namespace Application.Orders.Commands.RemoveLineItem;
public sealed record RemoveLineItemCommand(OrderId orderId, LineItemId LineItemId) : ICommand;
