using Application.Interfaces;
using Domain.Orders;

namespace Application.Orders.Queries.GetOrderSummary;
public record GetOrderSummaryQuery(OrderId OrderId) : IQuery<OrderSummary>;
