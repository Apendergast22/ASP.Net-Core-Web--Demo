namespace CriminalCheckerBackend.Model.Exceptions;

/// <summary>
/// This exception is being throw if validating DTO is invalid.
/// </summary>
public class InvalidDtoException : Exception
{
    /// <summary>
    /// Create new instance of <see cref="InvalidDtoException"/>.
    /// </summary>
    /// <param name="property">Invalid property name.</param>
    /// <param name="details">Exception details.</param>
    /// <param name="message">Detail report.</param>
    public InvalidDtoException(string property, string details, string message) : base(message)
    {
        Property = property;
        Details = details;
    }

    /// <summary>
    /// Get invalid property name.
    /// </summary>
    public string Property { get; }

    /// <summary>
    /// Get exception details.
    /// </summary>
    public string Details { get; }

}