using CriminalCheckerBackend.Model.DataBase;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CriminalCheckerBackend.Model.DTO;

/// <summary>
/// User info data model.
/// </summary>
public class UserDto
{
    /// <summary>
    /// Create new instance for <see cref="UserDto"/>.
    /// </summary>
    /// <param name="user"><see cref="RegisteredUser"/>.</param>
    public UserDto(RegisteredUser user)
    {
        Email = user.Email;
        UserId = user.UserId;
        Name = user.Name;
        Surname = user.Surname;
        Patronymic = user.Patronymic;
        BirthDay = user.BirthDay.ToDateTime(TimeOnly.MinValue);
        City = user.City;
        Address = user.Address;
    }

    /// <summary>
    /// Get or set user email.
    /// </summary>
    [Required]
    [JsonProperty("email")]
    public string Email { get; set; }

    /// <summary>
    /// Get or set user identifier.
    /// </summary>
    [Required]
    [JsonProperty("id")]
    public int UserId { get; set; }

    /// <summary>
    /// Get or set user's name.
    /// </summary>
    [Required]
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Get or set user's surname.
    /// </summary>
    [Required]
    [JsonProperty("surname")]
    public string Surname { get; set; }

    /// <summary>
    /// Get or set user's patronymic.
    /// </summary>
    [Required]
    [JsonProperty("patronymic")]
    public string Patronymic { get; set; }

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
    public string City { get; set; }

    /// <summary>
    /// Get or set address in user's city where user lives.
    /// </summary>
    [Required]
    [JsonProperty("address")]
    public string Address { get; set; }
}