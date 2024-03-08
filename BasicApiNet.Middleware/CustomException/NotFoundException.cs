using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BasicApiNet.Middleware.CustomException;

public class NotFoundException : Exception
{
    public NotFoundException() : base("Resource not found.")
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public static void ThrowIfNull([NotNull] object? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null)
        {
            Throw(paramName);
        }
    }

    private static void Throw(string? paramName) =>
        throw new NotFoundException(paramName);
}