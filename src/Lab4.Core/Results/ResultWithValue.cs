namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

public sealed class ResultWithValue<TValue>
{
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public string? ErrorMessage { get; }

    public TValue? Value { get; }

    private ResultWithValue(bool isSuccess, TValue? value, string? errorMessage)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorMessage = errorMessage;
    }

    public static ResultWithValue<TValue> Success(TValue value)
    {
        return new ResultWithValue<TValue>(true, value, null);
    }

    public static ResultWithValue<TValue> Failure(string errorMessage)
    {
        return new ResultWithValue<TValue>(false, default, errorMessage);
    }
}