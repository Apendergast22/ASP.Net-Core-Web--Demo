namespace CriminalCheckerBackend.Model.DataBase;

/// <summary>
/// Database drinker model.
/// </summary>
public class Drinker : BaseUserEntity
{
    /// <summary>
    /// Create new instance <see cref="Drinker"/>.
    /// </summary>
    /// <param name="id">User identifier.</param>
    public Drinker(int id) : base(id)
    {
        UserName = string.Empty;
        Surname = string.Empty;
        Patronymic = string.Empty;
        BirthDay = DateOnly.FromDateTime(DateTime.Today);
    }

    /// <summary>
    /// Instancing <see cref="Drinker"/>.
    /// </summary>
    /// <param name="id">User identifier.</param>
    /// <param name="userName">User's name.</param>
    /// <param name="surname">User's surname.</param>
    /// <param name="patronymic">User's patronymic.</param>
    /// <param name="birthDay">User's birth day.</param>
    public Drinker(int id, string userName, string surname, string patronymic, DateTime birthDay) : base(id)
    {
        UserName = userName;
        Surname = surname;
        Patronymic = patronymic;
        BirthDay = DateOnly.FromDateTime(birthDay);
    }
    
    /// <summary>
    /// Get or set user's name.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Get or set user's surname.
    /// </summary>
    public string Surname { get; set; }

    /// <summary>
    /// Get or set user's patronymic.
    /// </summary>
    public string Patronymic { get; set; }

    /// <summary>
    /// Get or set user's birth day.
    /// </summary>
    public DateOnly BirthDay { get; set; }
}