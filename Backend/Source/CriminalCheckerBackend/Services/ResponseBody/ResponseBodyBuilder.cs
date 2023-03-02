using CriminalCheckerBackend.Model.Exceptions;

namespace CriminalCheckerBackend.Services.ResponseBody;

public class ResponseBodyBuilder : IResponseBodyBuilder
{
    /// <summary>
    /// Response error type key.
    /// </summary>
    private const string ErrorType = "type";

    /// <summary>
    /// Response error type key.
    /// </summary>
    private const string ErrorUnknown = "unknown";

    /// <inheritdoc />
    public Dictionary<string, dynamic> Build(InvalidDtoException exception)
    {
        return new Dictionary<string, dynamic>
        {
            { ErrorUnknown, false },
            { ErrorType, "BadProperty" },
            { "property", exception.Property },
            { "details", exception.Details }
        };
    }

    /// <inheritdoc />
    public Dictionary<string, dynamic> Build()
    {
        return new Dictionary<string, dynamic> { { ErrorUnknown, false } };
    }

    /// <inheritdoc />
    public Dictionary<string, dynamic> Build(string type)
    {
        return new Dictionary<string, dynamic> { { ErrorUnknown, false }, { ErrorType, type } };
    }
}