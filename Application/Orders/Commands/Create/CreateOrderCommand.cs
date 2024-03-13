using Application.Interfaces;
using Domain.Customers;

namespace Application.Orders.Commands.Create;
public sealed record CreateOrderCommand(CustomerId CustomerId) : ICommand;
