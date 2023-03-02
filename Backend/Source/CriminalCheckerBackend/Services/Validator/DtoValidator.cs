using CriminalCheckerBackend.Model.DTO;
using System.Net.Mail;
using System.Text.RegularExpressions;
using CriminalCheckerBackend.Model.Exceptions;

namespace CriminalCheckerBackend.Services.Validator;

/// <summary>
/// DTO validator.
/// </summary>
public class DtoValidator : IDtoValidator
{
    /// <summary>
    /// The latest date of birth.
    /// </summary>
    private static readonly DateTime MaxDate = DateTime.Today.Subtract(TimeSpan.FromDays(7 * 365));

    /// <summary>
    /// The earliest date of birth.
    /// </summary>
    private static readonly DateTime MinDate = DateTime.Today.Subtract(TimeSpan.FromDays(200 * 365));

    /// <inheritdoc />
    public void Validate(SignUpDto dto)
    {
        CheckEmail(dto.Email);
        CheckBirthDay(dto.BirthDay);
        CheckPassword(dto.Password);

        CheckNullOrEmpty(nameof(dto.Address), "Address", dto.Address);
        CheckNullOrEmpty(nameof(dto.City), "Town", dto.City);
        CheckNullOrEmpty(nameof(dto.Name), "Name", dto.Name);
        CheckNullOrEmpty(nameof(dto.Surname), "Surname", dto.Surname);

        CheckNoSpecial(nameof(dto.Address), "Address", dto.Address);
        CheckNoSpecial(nameof(dto.City), "Town", dto.City);
        CheckNoSpecial(nameof(dto.Name), "Name", dto.Name);
        CheckNoSpecial(nameof(dto.Surname), "Surname", dto.Surname);

        CheckNoDigit(nameof(dto.Name), "Name", dto.Name);
        CheckNoDigit(nameof(dto.Surname), "Surname", dto.Surname);
    }

    /// <inheritdoc />
    public void Validate(SignInDto dto)
    {
        CheckEmail(dto.Email);
        CheckPassword(dto.Password);
    }

    /// <inheritdoc />
    public void Validate(DrinkerDto dto)
    {
        CheckBirthDay(dto.BirthDay);

        CheckNullOrEmpty(nameof(dto.Name), "Name", dto.Name);
        CheckNullOrEmpty(nameof(dto.Surname), "Surname", dto.Surname);

        CheckNoSpecial(nameof(dto.Name), "Name", dto.Name);
        CheckNoSpecial(nameof(dto.Surname), "Surname", dto.Surname);

        CheckNoDigit(nameof(dto.Name), "Name", dto.Name);
        CheckNoDigit(nameof(dto.Surname), "Surname", dto.Surname);
    }

    /// <summary>
    /// Checking for empty property.
    /// </summary>
    /// <param name="property">Property name.</param>
    /// <param name="name">Property adapted name.</param>
    /// <param name="value">Property value.</param>
    /// <exception cref="InvalidDtoException"><see cref="InvalidDtoException"/>.</exception>
    private void CheckNullOrEmpty(string property, string name, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidDtoException(property, $"Property {name} can't be null or empty", string.Empty);
    }

    /// <summary>
    /// Checking user birth day.
    /// </summary>
    /// <param name="birthDay"></param>
    /// <exception cref="InvalidDtoException"><see cref="InvalidDtoException"/>.</exception>
    private void CheckBirthDay(DateTime birthDay)
    {
        if (birthDay < MinDate)
            throw new InvalidDtoException("BirthDay", "You can't be older than 200 years", string.Empty);
        
        if (birthDay > MaxDate)
            throw new InvalidDtoException("BirthDay", "You're too young! Service doesn't", string.Empty);
    }

    /// <summary>
    /// Checking email address.
    /// </summary>
    /// <param name="value">Email address.</param>
    /// <exception cref="InvalidDtoException"><see cref="InvalidDtoException"/>.</exception>
    private void CheckEmail(string value)
    {
        try
        {
            var mailAddress = new MailAddress(value);
        }
        catch (FormatException)
        {
            throw new InvalidDtoException("Email", "Incorrect email", string.Empty);
        }
    }

    /// <summary>
    /// Checking value for non-digit and non-alphabetic symbols.
    /// </summary>
    /// <param name="property">Property name.</param>
    /// <param name="name">Property adapted name.</param>
    /// <param name="value">Property value.</param>
    /// <exception cref="InvalidDtoException"><see cref="InvalidDtoException"/>.</exception>
    private void CheckNoSpecial(string property, string name, string value)
    {
        if (Regex.IsMatch(value, @"\W"))
            throw new InvalidDtoException(property, $"{name} contains special symbols!", string.Empty);
    }

    /// <summary>
    /// Checking value for digit symbols.
    /// </summary>
    /// <param name="property">Property name.</param>
    /// <param name="name">Property adapted name.</param>
    /// <param name="value">Property value.</param>
    /// <exception cref="InvalidDtoException"><see cref="InvalidDtoException"/>.</exception>
    private void CheckNoDigit(string property, string name, string value)
    {
        if (Regex.IsMatch(value, @"\d"))
            throw new InvalidDtoException(property, $"{name} contains digits!", string.Empty);
    }

    /// <summary>
    /// Checking password.
    /// </summary>
    /// <param name="value">Password.</param>
    /// <exception cref="InvalidDtoException"><see cref="InvalidDtoException"/>.</exception>
    private void CheckPassword(string value)
    {
        const int minPasswordLen = 5;
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidDtoException("Password", "Password can't be empty!", string.Empty);

        if(value.Length < minPasswordLen)
            throw new InvalidDtoException("Password", "Password is too short! Password length have to be more than 4 characters!", string.Empty);
    }
}