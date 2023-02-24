namespace CriminalCheckerBackend.Model;

/// <summary>
/// Model of information from application json.
/// </summary>
public class DataBaseInfo
{
    /// <summary>
    /// Get or set database connection string.
    /// </summary>
    public string? ConnectionString { get; set; }

    /// <summary>
    /// Get or set count of attempts to connection with database.
    /// </summary>
    public int MaxRetryCount { get; set; }

    /// <summary>
    /// Get or set connection timeout.
    /// </summary>
    public int CommandTimeout { get; set; }

    /// <summary>
    /// Get or set value is indicating that enables detailed
    /// errors when handling of data value exceptions that
    /// occur during processing of store query results.
    /// </summary>
    public bool EnableDetailedErrors { get; set; }
}