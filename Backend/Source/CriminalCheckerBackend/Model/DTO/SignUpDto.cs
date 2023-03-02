using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CriminalCheckerBackend.Model.DTO;

/// <summary>
/// Registration new user info.
/// </summary>
public class SignUpDto
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
    /// Get or set user's name.
    /// </summary>
    [Required]
    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Get or set user's surname.
    /// </summary>
    [Required]
    [JsonProperty("surname")]
    public string Surname { get; set; } = null!;

    /// <summary>
    /// Get or set user's patronymic.
    /// </summary>
    [Required]
    [JsonProperty("patronymic")]
    public string Patronymic { get; set; } = null!;

    /// <summary>
    /// Get or set user's birth day.
    /// </summary>
    [JsonProperty("date")]
    [Required]
    public DateTime BirthDay { get; set; }

    /// <summary>
    /// Get or set city where user lives.
    /// </summary>
    [Required]
    [JsonProperty("city")] 
    public string City { get; set; } = null!;

    /// <summary>
    /// Get or set address in user's city where user lives.
    /// </summary>
    [Required]
    [JsonProperty("address")]
    public string Address { get; set; } = null!;
}