namespace CriminalCheckerBackend.Model.DataBase;

/// <summary>
/// Database registered user model.
/// </summary>
public class RegisteredUser : BaseUserEntity
{
    /// <summary>
    /// Empty constructor.
    /// </summary>
    public RegisteredUser()
    {
        Email = string.Empty;
        Hash = Array.Empty<byte>();
        Name = string.Empty;
        Surname = string.Empty;
        Patronymic = string.Empty;
        City = string.Empty;
        Address = string.Empty;
    }

    /// <summary>
    /// Empty constructor.
    /// </summary>
    /// <param name="id">User identifier.</param>
    /// <param name="info"><see cref="NewUserInfo"/>.</param>
    public RegisteredUser(int id, NewUserInfo info) : base(id)
    {
        Email = info.Email;
        Hash = info.Hash;
        Name = info.Name;
        Surname = info.Surname;
        Patronymic = info.Patronymic;
        City = info.City;
        Address = info.Address;
    }

    /// <summary>
    /// Get or set user's email.
    /// </summary>
    public string Email { get; }

    /// <summary>
    /// Get or set hashed user's password.
    /// </summary>
    public byte[] Hash { get; }

    /// <summary>
    /// Get or set user's name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Get or set user's surname.
    /// </summary>
    public string Surname { get; }

    /// <summary>
    /// Get or set user's patronymic.
    /// </summary>
    public string Patronymic { get; }

    /// <summary>
    /// Get or set user's birth day.
    /// </summary>
    public DateTime BirthDay { get; }

    /// <summary>
    /// Get or set city where user lives.
    /// </summary>
    public string City { get; }

    /// <summary>
    /// Get or set address in user's city where user lives.
    /// </summary>
    public string Address { get; }
}