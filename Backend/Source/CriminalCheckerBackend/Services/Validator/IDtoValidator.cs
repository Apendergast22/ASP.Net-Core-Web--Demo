using CriminalCheckerBackend.Model.DTO;
using CriminalCheckerBackend.Model.Exceptions;

namespace CriminalCheckerBackend.Services.Validator;

/// <summary>
/// Interface for DTO validators.
/// </summary>
public interface IDtoValidator
{
    /// <summary>
    /// Sign Up DTO validation.
    /// </summary>
    /// <param name="dto"><see cref="SignUpDto"/>.</param>
    /// <exception cref="InvalidDtoException"><see cref="InvalidDtoException"/>.</exception>
    void Validate(SignUpDto dto);

    /// <summary>
    /// Sign In DTO validation.
    /// </summary>
    /// <param name="dto"><see cref="SignInDto"/>.</param>
    /// <exception cref="InvalidDtoException"><see cref="InvalidDtoException"/>.</exception>
    void Validate(SignInDto dto);

    /// <summary>
    /// Drinker DTO validation.
    /// </summary>
    /// <param name="dto"><see cref="DrinkerDto"/>.</param>
    /// <exception cref="InvalidDtoException"><see cref="InvalidDtoException"/>.</exception>
    void Validate(DrinkerDto dto);
}