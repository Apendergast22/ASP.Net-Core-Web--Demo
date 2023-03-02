using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        BirthDay = DateOnly.MinValue;
    }

    /// <summary>
    /// Empty constructor.
    /// </summary>
    /// <param name="info"><see cref="NewUserInfo"/>.</param>
    public RegisteredUser(NewUserInfo info)
    {
        Email = info.Email;
        Hash = info.Hash;
        Name = info.Name;
        Surname = info.Surname;
        Patronymic = info.Patronymic;
        City = info.City;
        Address = info.Address;
        BirthDay = DateOnly.FromDateTime(info.BirthDay);
    }

    /// <summary>
    /// Get or set user's email.
    /// </summary>
    [Column("Email", TypeName = "text")]
    public string Email { get; }

    /// <summary>
    /// Get or set hashed user's password.
    /// </summary>
    [MaxLength(16)]
    [Column("Hash", TypeName = "bytea")]
    public byte[] Hash { get; }

    /// <summary>
    /// Get or set user's name.
    /// </summary>
    [Column("Name", TypeName = "text")]
    public string Name { get; }

    /// <summary>
    /// Get or set user's surname.
    /// </summary>
    [Column("Surname", TypeName = "text")]
    public string Surname { get; }

    /// <summary>
    /// Get or set user's patronymic.
    /// </summary>
    [Column("Patronymic", TypeName = "text")]
    public string Patronymic { get; }

    /// <summary>
    /// Get or set user's birth day.
    /// </summary>
    [Column("BirthDay", TypeName = "date")]
    public DateOnly BirthDay { get; }

    /// <summary>
    /// Get or set city where user lives.
    /// </summary>
    [Column("City", TypeName = "text")]
    public string City { get; }

    /// <summary>
    /// Get or set address in user's city where user lives.
    /// </summary>
    [Column("Address", TypeName = "text")]
    public string Address { get; }
}