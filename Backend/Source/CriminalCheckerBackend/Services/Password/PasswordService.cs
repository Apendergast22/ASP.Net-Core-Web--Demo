using System.Security.Cryptography;
using System.Text;
using CriminalCheckerBackend.Model.Errors;
using JetBrains.Annotations;

namespace CriminalCheckerBackend.Services.Password;

/// <summary>
/// Service of passwords processing.
/// </summary>
public class PasswordService : IPassword
{
    /// <summary>
    /// Path to file with salt.
    /// </summary>
    private readonly string _pathToSalt;

    /// <summary>
    /// Number of rows in text file with salt.
    /// </summary>
    private readonly int _saltItemsCount;

    /// <summary>
    /// Creating new instance of <see cref="PasswordService"/>.
    /// </summary>
    /// <param name="pathToSalt">Path to file with salt.</param>
    /// <param name="saltItemsCount">Number of rows in text file with salt.</param>
    public PasswordService([NotNull] string pathToSalt, int saltItemsCount)
    {
        if (string.IsNullOrWhiteSpace(pathToSalt))
            throw new ArgumentNullException(nameof(pathToSalt));

        _pathToSalt = pathToSalt;
        _saltItemsCount = saltItemsCount;
    }

    /// <inheritdoc />
    public byte[] Hash(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new BadPasswordException();

        var bytePassword = Encoding.UTF8.GetBytes(password);
        var salt = ReadSalt();

        var algorithm = SHA256.Create();
        var plainTextWithSaltBytes = new byte[bytePassword.Length + salt.Length];
        for (var i = 0; i < bytePassword.Length; i++)
            plainTextWithSaltBytes[i] = bytePassword[i];
        
        for (var i = 0; i < salt.Length; i++)
            plainTextWithSaltBytes[bytePassword.Length + i] = salt[i];

        return algorithm.ComputeHash(plainTextWithSaltBytes);
    }

    /// <inheritdoc />
    public bool VerifyPassword(string password, byte[] hash)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new BadPasswordException();

        if (hash == null || hash.Length == 0)
            throw new ArgumentNullException(nameof(hash));

        var hashedPassword = Hash(password);
        if (hashedPassword.Length != hash.Length)
            return false;
        
        return !hashedPassword.Where((t, i) => t != hash[i]).Any();
    }

    /// <summary>
    /// Read any salt byte array from text storage.
    /// </summary>
    /// <returns>Salt.</returns>
    private byte[] ReadSalt()
    {
        var skip = Random.Shared.Next(0, _saltItemsCount - 2);
        return Encoding.UTF8.GetBytes(File.ReadAllLines(_pathToSalt, Encoding.UTF8).Skip(skip).Take(1).Single());
    }
}