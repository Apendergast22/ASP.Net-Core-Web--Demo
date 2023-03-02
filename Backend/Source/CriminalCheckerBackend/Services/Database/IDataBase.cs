using CriminalCheckerBackend.Model;
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
    /// Get or set collection <see cref="Drinker"/>.
    /// </summary>
    [NotNull]
    [ItemNotNull]
    public DbSet<Drinker> Drinkers { get; set; }

    /// <summary>
    /// Get or set collection <see cref="RegisteredUser"/>.
    /// </summary>
    [NotNull]
    [ItemNotNull]
    public DbSet<RegisteredUser> RegisteredUsers { get; set; }

    /// <summary>
    /// Recreation data base.
    /// </summary>
    public void RecreateDataBase();

    /// <summary>
    /// Checking user in drinkers collection.
    /// </summary>
    /// <param name="user"><see cref="DrinkerDto"/>.</param>
    /// <returns><see cref="Task"/> for calculating value, that shows target user is drinker.</returns>
    Task<bool> DoesUserDrinkerAsync([NotNull] DrinkerDto user);
    
    /// <summary>
    /// Adding new drinker.
    /// </summary>
    /// <param name="name">Drinker's name.</param>
    /// <param name="surname">Drinker's surname.</param>
    /// <param name="patronymic">Drinker's patronymic.</param>
    /// <param name="birthDay">Drinker's birth day.</param>
    /// <returns></returns>
    Task AddNewDrinkerAsync(string name, string surname, string patronymic, DateOnly birthDay);

    /// <summary>
    /// Registration new user.
    /// </summary>
    /// <param name="data"><see cref="NewUserInfo"/>.</param>
    /// <returns><see cref="Task"/>.</returns>
    /// <exception cref="UserExistsException"></exception>
    Task RegistrationNewUserAsync([NotNull] NewUserInfo data);
}