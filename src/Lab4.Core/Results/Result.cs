namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

public sealed class Result
{
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public string? ErrorMessage { get; }

    private Result(bool isSuccess, string? errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public static Result Success()
    {
        return new Result(true, null);
    }

    public static Result Failure(string errorMessage)
    {
        return new Result(false, errorMessage);
    }
}