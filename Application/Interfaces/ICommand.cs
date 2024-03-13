﻿using Domain.Shared;
using MediatR;

namespace Application.Interfaces;
public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}