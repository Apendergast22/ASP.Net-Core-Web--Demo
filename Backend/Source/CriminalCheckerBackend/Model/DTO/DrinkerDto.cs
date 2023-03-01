
using JetBrains.Annotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CriminalCheckerBackend.Model.DTO;

/// <summary>
/// User info model.
/// </summary>
public class DrinkerDto
{
    /// <summary>
    /// Instancing <see cref="DrinkerDto"/>.
    /// </summary>
    /// <param name="id">User identifier.</param>
    /// <param name="name">User's name.</param>
    /// <param name="surname">User's surname.</param>
    /// <param name="patronymic">User's patronymic.</param>
    /// <param name="birthDay">User's birth day.</param>
    public DrinkerDto([CanBeNull] int? id, string name, string surname, string patronymic, DateTime birthDay)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
        BirthDay = birthDay;
    }

    /// <summary>
    /// Get or set user's identifier.
    /// </summary>
    [CanBeNull]
    [JsonProperty("id")]
    public int? Id { get; set; }

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
    [JsonProperty("patronymic")]
    public string Patronymic { get; set; }

    /// <summary>
    /// Get or set user's birth day.
    /// </summary>
    [Required]
    [JsonProperty("date")]
    public DateTime BirthDay { get; set; }
}