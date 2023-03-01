namespace CriminalCheckerBackend.Services.Route;

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
    Task<ulong> CalculateAsync(string city, string address);
}