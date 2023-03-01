namespace CriminalCheckerBackend.Model.Config;

/// <summary>
/// Configuration of TomTom API service.
/// </summary>
public class TomTomInfo
{
    /// <summary>
    /// Get or set API access token.
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// Get or set base URI of API service.
    /// </summary>
    public string? BaseUri { get; set; }
}