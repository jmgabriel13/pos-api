using Domain.Shared;
using MediatR;

namespace Application.Interfaces;

// References
// - CQRS, command and query, always have to return a Result object
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
