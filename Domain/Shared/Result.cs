﻿namespace Domain.Shared;

// References
// - Domain Validation, Clean Architecture, DDD
// - vkhorikov/CSharpFunctionalExtensions
// - m-jovanovic/rally-simulator

// 1st option for implementing Domain Validation

/// <summary>
/// More explicit or expressive
/// Performant, it just return the failure result
/// Self-documenting, document possible errors
/// ---No stack trace, but this can be mitigated with proper logging
/// </summary>
public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static Result Success() => new(true, Error.None);

    public static Result<TValue> Success<TValue>(TValue? value) => new(value, true, Error.None);

    public static Result<TValue> Create<TValue>(TValue? value, Error error)
        where TValue : class =>
        value is null ? Failure<TValue>(error) : Success(value);

    public static Result Failure(Error error) => new(false, error);

    public static Result<TValue> Failure<TValue>(Error error) => new(default!, false, error!);

    public static Result FirstFailureOrSuccess(params Result[] results)
    {
        foreach (Result result in results)
        {
            if (result.IsFailure)
            {
                return result;
            }

        }

        return Success();
    }

}
