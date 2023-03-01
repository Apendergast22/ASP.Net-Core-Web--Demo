namespace CriminalCheckerBackend.Services.Route;

/// <summary>
/// Service for calculation route time.
/// </summary>
public class RouteCalculator : IRouteCalculator
{
    /// <inheritdoc />
    public Task<ulong> CalculateAsync(string city, string address)
    {
        throw new NotImplementedException();
    }
}