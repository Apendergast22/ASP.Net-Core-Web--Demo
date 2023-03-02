namespace CriminalCheckerBackend.Model.Config;

/// <summary>
/// Configuration DaData API service.
/// </summary>
public class DaDataInfo
{
    /// <summary>
    /// Get or set secret key.
    /// </summary>
    public string? SecretKey { get; set; }

    /// <summary>
    /// Get or set API key.
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// Get or set base URI of API service.
    /// </summary>
    public string? BaseUri { get; set; }
}