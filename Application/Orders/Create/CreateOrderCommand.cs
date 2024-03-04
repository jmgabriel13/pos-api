using Application.Interfaces;
using Domain.Customers;

namespace Application.Orders.Create;
public sealed record CreateOrderCommand(CustomerId CustomerId) : ICommand;
