using CriminalCheckerBackend.Model.DTO;

namespace CriminalCheckerBackend.Model;

/// <summary>
/// Information about new user.
/// </summary>
public class NewUserInfo
{
    /// <summary>
    /// Create new instance of <see cref="NewUserInfo"/>.
    /// </summary>
    /// <param name="dto"><see cref="SignUpDto"/>.</param>
    /// <param name="hashedPassword">Hashed user password.</param>
    /// <param name="saltPosition">Salt position in file.</param>
    public NewUserInfo(SignUpDto dto, byte[] hashedPassword, int saltPosition)
    {
        Email = dto.Email;
        Name = dto.Name;
        Surname = dto.Surname;
        Patronymic = dto.Patronymic;
        BirthDay = dto.BirthDay;
        City = dto.City;
        Address = dto.Address;
        Hash = hashedPassword;
        SaltPosition = saltPosition;
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
    /// Get or set salt position in file.
    /// </summary>
    public int SaltPosition { get; }

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