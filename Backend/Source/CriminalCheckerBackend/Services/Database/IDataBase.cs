using CriminalCheckerBackend.Model.DataBase;
using CriminalCheckerBackend.Model.DataBase.Exceptions;
using CriminalCheckerBackend.Model.DTO;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace CriminalCheckerBackend.Services.Database;

/// <summary>
/// Interface for databases.
/// </summary>
public interface IDataBase
{
    /// <summary>
    /// Get or set collection <see cref="UserData"/>.
    /// </summary>
    [NotNull]
    public DbSet<UserData> UserDataValues { get; set; }

    /// <summary>
    /// Checking user in drinkers collection.
    /// </summary>
    /// <param name="user"><see cref="UserRequest"/>.</param>
    /// <returns><see cref="Task"/> for calculating value, that shows target user is drinker.</returns>
    /// <exception cref="NewUserNotValidValueException"></exception>
    Task<bool> DoesUserDrinkerAsync([NotNull] UserRequest user);

    /// <summary>
    /// Registration new user.
    /// </summary>
    /// <param name="data"><see cref="UserRegistrationData"/>.</param>
    /// <returns><see cref="Task"/>.</returns>
    /// <exception cref="NewUserNotValidValueException"></exception>
    /// <exception cref="UserExistsException"></exception>
    Task RegistrationNewUserAsync([NotNull] UserRegistrationData data);
}