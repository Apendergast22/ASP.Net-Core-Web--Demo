namespace CriminalCheckerBackend.Model.Config;

/// <summary>
/// Configuration for password service.
/// </summary>
public class PasswordServiceInfo
{
    /// <summary>
    /// Get or set path to file with salt.
    /// </summary>
    public string? PathToSalt { get; set; }
}