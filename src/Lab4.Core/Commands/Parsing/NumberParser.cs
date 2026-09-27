using Itmo.ObjectOrientedProgramming.Lab4.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Parsing;

public sealed class NumberParser
{
    public ResultWithValue<int> ParsePositiveInteger(string text, string valueName)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            string message = valueName + " must be a positive integer.";
            return ResultWithValue<int>.Failure(message);
        }

        string trimmed = text.Trim();
        int value = 0;

        for (int index = 0; index < trimmed.Length; index++)
        {
            char character = trimmed[index];

            if (character < '0' || character > '9')
            {
                string message = valueName + " must contain only digits.";
                return ResultWithValue<int>.Failure(message);
            }

            int digit = character - '0';

            value = (value * 10) + digit;
        }

        if (value <= 0)
        {
            string message = valueName + " must be greater than zero.";
            return ResultWithValue<int>.Failure(message);
        }

        return ResultWithValue<int>.Success(value);
    }
}