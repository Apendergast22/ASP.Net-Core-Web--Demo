using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CriminalCheckerBackend.Model.DTO;

/// <summary>
/// An <see cref="StatusCodeResult"/> that when executed will produce an empty
/// <see cref="StatusCodes.Status200OK"/> response.
/// </summary>
[DefaultStatusCode(DefaultStatusCode)]
public class UserResponse : StatusCodeResult
{
    /// <summary>
    /// Default status code.
    /// </summary>
    private const int DefaultStatusCode = StatusCodes.Status200OK;

    /// <summary>
    /// Get or set value is indicating that user is a drinker.
    /// </summary>
    public bool DoesUserDrinker { get; set; }

    /// <summary>
    /// Instance new object of <see cref="UserResponse"/>.
    /// </summary>
    /// <param name="doesUserDrinker">.</param>
    public UserResponse(bool doesUserDrinker) : base(DefaultStatusCode)
    {
        DoesUserDrinker = doesUserDrinker;
    }
}