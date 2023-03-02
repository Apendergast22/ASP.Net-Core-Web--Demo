using CriminalCheckerBackend.Model.Exceptions;
using JetBrains.Annotations;

namespace CriminalCheckerBackend.Services.Password;

/// <summary>
/// Interface for password services.
/// </summary>
public interface IPassword
{
    /// <summary>
    /// Calculate password hash.
    /// </summary>
    /// <param name="password">Hashing password.</param>
    /// <returns>Hashed password.</returns>
    /// <exception cref="BadPasswordException"><see cref="BadPasswordException"/></exception>
    /// <exception cref="FileNotFoundException">Salt file wasn't found.</exception>
    byte[] Hash([NotNull] string password);

    /// <summary>
    /// Verification passwords
    /// </summary>
    /// <param name="password">Verifiable password.</param>
    /// <param name="hash">Hash for compassion.</param>
    /// <returns>Value is indicating that password was verified.</returns>
    /// <exception cref="BadPasswordException"><see cref="BadPasswordException"/></exception>
    /// <exception cref="FileNotFoundException">Salt file wasn't found.</exception>
    bool VerifyPassword([NotNull] string password, [NotNull] byte[] hash);
}