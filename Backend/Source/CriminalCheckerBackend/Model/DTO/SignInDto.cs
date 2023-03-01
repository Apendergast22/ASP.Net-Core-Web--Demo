using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CriminalCheckerBackend.Model.DTO;

/// <summary>
/// Authorization data model.
/// </summary>
public class SignInDto
{
    /// <summary>
    /// Get or set user email.
    /// </summary>
    [Required]
    [JsonProperty("email")]
    public string Email { get; set; } = null!;

    /// <summary>
    /// Get or set user password.
    /// </summary>
    [Required]
    [JsonProperty("password")]
    public string Password { get; set; } = null!;

    /// <summary>
    /// Get or set value is indicating that user want to be signed in for a long time.
    /// </summary>
    [Required]
    [JsonProperty("keep")]
    public bool KeepSign { get; set; }
}