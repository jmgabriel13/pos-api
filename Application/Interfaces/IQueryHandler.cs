﻿using Domain.Shared;
using MediatR;

namespace Application.Interfaces;
public interface IQueryHandler<TQuery, TResponse> : 
    IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}