using Domain.Shared;
using MediatR;

namespace Application.Interfaces;

// References
// - CQRS, command and query, always have to return a Result object
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}
