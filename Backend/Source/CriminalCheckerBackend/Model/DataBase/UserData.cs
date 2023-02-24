namespace CriminalCheckerBackend.Model.DataBase;

/// <summary>
/// Database user model.
/// </summary>
public class UserData
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public UserData()
    {
    }

    /// <summary>
    /// Instancing <see cref="UserData"/>.
    /// </summary>
    /// <param name="userName">User's name.</param>
    /// <param name="surname">User's surname.</param>
    /// <param name="patronymic">User's patronymic.</param>
    /// <param name="birthDay">User's birth day.</param>
    public UserData(string userName, string surname, string patronymic, DateTime birthDay)
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