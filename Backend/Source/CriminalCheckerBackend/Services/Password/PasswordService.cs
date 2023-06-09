﻿using System.Security.Cryptography;
using System.Text;
using CriminalCheckerBackend.Model.Exceptions;
using JetBrains.Annotations;

namespace CriminalCheckerBackend.Services.Password;

/// <summary>
/// Service of passwords processing.
/// </summary>
public class PasswordService : IPassword
{
    /// <summary>
    /// Minimal count of items in salt file.
    /// </summary>
    private const int MinItemsCount = 20;

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
        
        if (saltItemsCount <= MinItemsCount)
            throw new ArgumentOutOfRangeException(nameof(saltItemsCount));

        _pathToSalt = pathToSalt;
        _saltItemsCount = saltItemsCount;
    }

    /// <inheritdoc />
    public (int, byte[]) Hash(string password, int saltPosition)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new BadPasswordException();

        var bytePassword = Encoding.UTF8.GetBytes(password);
        var (position, salt) = ReadSalt(saltPosition);

        var plainTextWithSaltBytes = new byte[bytePassword.Length + salt.Length];
        for (var i = 0; i < bytePassword.Length; i++)
            plainTextWithSaltBytes[i] = bytePassword[i];
        
        for (var i = 0; i < salt.Length; i++)
            plainTextWithSaltBytes[bytePassword.Length + i] = salt[i];

        var algorithm = SHA256.Create();
        return (position, algorithm.ComputeHash(plainTextWithSaltBytes));
    }

    /// <inheritdoc />
    public bool VerifyPassword(string password, byte[] hash, int saltPosition)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new BadPasswordException();

        if (hash == null || hash.Length == 0)
            throw new ArgumentNullException(nameof(hash));

        var (position, hashedPassword) = Hash(password, saltPosition);
        if (hashedPassword.Length != hash.Length)
            return false;
        
        return !hashedPassword.Where((t, i) => t != hash[i]).Any();
    }

    /// <summary>
    /// Read any salt byte array from text storage.
    /// </summary>
    /// <param name="position">Salt position in salt file.</param>
    /// <returns>Salt position ant salt.</returns>
    private (int, byte[]) ReadSalt(int position = -1)
    {
        if (!File.Exists(_pathToSalt))
            throw new FileNotFoundException();

        var skip = position == -1 ? Random.Shared.Next(0, _saltItemsCount - 2) : position;
        return  (skip, Encoding.UTF8.GetBytes(File.ReadAllLines(_pathToSalt, Encoding.UTF8).Skip(skip).Take(1).Single()));
    }
}