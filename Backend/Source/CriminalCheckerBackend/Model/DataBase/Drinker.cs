using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CriminalCheckerBackend.Model.DataBase;

/// <summary>
/// Database drinker model.
/// </summary>
public class Drinker
{
    /// <summary>
    /// Empty constructor for entity framework.
    /// </summary>
    public Drinker()
    {
        UserName = string.Empty;
        Surname = string.Empty;
        Patronymic = string.Empty;
        BirthDay = DateOnly.FromDateTime(DateTime.Today);
        UserId = -1;
    }

    /// <summary>
    /// Create new instance <see cref="Drinker"/>.
    /// </summary>
    /// <param name="id">User identifier.</param>
    public Drinker(int id)
    {
        UserName = string.Empty;
        Surname = string.Empty;
        Patronymic = string.Empty;
        BirthDay = DateOnly.FromDateTime(DateTime.Today);
        UserId = id;
    }

    /// <summary>
    /// Instancing <see cref="Drinker"/>.
    /// </summary>
    /// <param name="id">User identifier.</param>
    /// <param name="userName">User's name.</param>
    /// <param name="surname">User's surname.</param>
    /// <param name="patronymic">User's patronymic.</param>
    /// <param name="birthDay">User's birth day.</param>
    public Drinker(int id, string userName, string surname, string patronymic, DateOnly birthDay)
    {
        UserName = userName;
        Surname = surname;
        Patronymic = patronymic;
        BirthDay = birthDay;
        UserId = id;
    }

    /// <summary>
    /// Get or set database index.
    /// </summary>
    [Key]
    [Column("Index", TypeName = "integer")]
    public int Index { get; set; }

    /// <summary>
    /// Get or set user id.
    /// </summary>
    [Column("UserId", TypeName = "integer")]
    public int UserId { get; set; }

    /// <summary>
    /// Get or set user's name.
    /// </summary>
    [Column("UserName", TypeName = "text")]
    public string UserName { get; set; }

    /// <summary>
    /// Get or set user's surname.
    /// </summary>
    [Column("Surname", TypeName = "text")]
    public string Surname { get; set; }

    /// <summary>
    /// Get or set user's patronymic.
    /// </summary>
    [Column("Patronymic", TypeName = "text")]
    public string Patronymic { get; set; }

    /// <summary>
    /// Get or set user's birth day.
    /// </summary>
    [Column("BirthDay", TypeName="date")]
    public DateOnly BirthDay { get; set; }
}