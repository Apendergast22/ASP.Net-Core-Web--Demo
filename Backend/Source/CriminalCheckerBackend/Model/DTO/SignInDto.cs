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
    public string Email { get; set; } = null!;

    /// <summary>
    /// Get or set user password.
    /// </summary>
    [Required]
    public string Password { get; set; } = null!;
}