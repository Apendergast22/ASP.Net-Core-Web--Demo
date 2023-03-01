using CriminalCheckerBackend.Model.Exceptions;

namespace CriminalCheckerBackend.Services.TomTomApi.Route;

/// <summary>
/// Interface for calculation route time services.
/// </summary>
public interface IRouteCalculator
{
    /// <summary>
    /// Calculation route time.
    /// </summary>
    /// <param name="city">User town.</param>
    /// <param name="address">User city.</param>
    /// <returns><see cref="Task"/> with route seconds.</returns>
    /// <exception cref="DaDataNotFoundException"><see cref="DaDataNotFoundException"/>.</exception>
    /// <exception cref="TomTomException"><see cref="TomTomException"/>.</exception>
    Task<int> CalculateRouteAsync(string city, string address);
}