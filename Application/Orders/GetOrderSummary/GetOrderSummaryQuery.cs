using Application.Interfaces;
using Domain.Orders;

namespace Application.Orders.GetOrderSummary;
public record GetOrderSummaryQuery(OrderId OrderId) : IQuery<OrderSummary>;
