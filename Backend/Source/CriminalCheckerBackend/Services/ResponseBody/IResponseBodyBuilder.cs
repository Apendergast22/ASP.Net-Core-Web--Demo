using CriminalCheckerBackend.Model.Exceptions;

namespace CriminalCheckerBackend.Services.ResponseBody;

/// <summary>
/// Interface of builder for response body
/// </summary>
public interface IResponseBodyBuilder
{
    /// <summary>
    /// Build body for response: <exception cref="InvalidDtoException">.</exception>.
    /// </summary>
    /// <param name="exception"><see cref="InvalidDtoException"/>.</param>
    /// <returns>Result body.</returns>
    Dictionary<string, dynamic> Build(InvalidDtoException exception);

    /// <summary>
    /// Build body for response: unknown error.
    /// </summary>
    /// <returns>Result body.</returns>
    Dictionary<string, dynamic> Build();

    /// <summary>
    /// Build body for response: any error.
    /// </summary>
    /// <returns>Result body.</returns>
    Dictionary<string, dynamic> Build(string type);
}