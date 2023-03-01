
using Newtonsoft.Json;

namespace CriminalCheckerBackend.Model.DTO;

/// <summary>
/// User info model.
/// </summary>
public class UserRequest
{
    /// <summary>
    /// Instancing <see cref="UserRequest"/>.
    /// </summary>
    /// <param name="name">User's name.</param>
    /// <param name="surname">User's surname.</param>
    /// <param name="patronymic">User's patronymic.</param>
    /// <param name="birthDay">User's birth day.</param>
    public UserRequest(string name, string surname, string patronymic, DateTime birthDay)
    {
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
        BirthDay = birthDay;
    }

    /// <summary>
    /// Get or set user's name.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Get or set user's surname.
    /// </summary>
    [JsonProperty("surname")]
    public string Surname { get; set; }

    /// <summary>
    /// Get or set user's patronymic.
    /// </summary>
    [JsonProperty("patronymic")]
    public string Patronymic { get; set; }

    /// <summary>
    /// Get or set user's birth day.
    /// </summary>
    [JsonProperty("date")]
    public DateTime BirthDay { get; set; }
}