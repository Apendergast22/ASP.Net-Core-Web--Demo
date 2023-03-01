using CriminalCheckerBackend.Model.DTO.TomTom;
using CriminalCheckerBackend.Model.Exceptions;
using CriminalCheckerBackend.Services.TomTomApi.Route;
using Dadata;
using Dadata.Model;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace CriminalCheckerBackend.Services.TomTomApi;

/// <summary>
/// TomTom API client.
/// <see href="https://dadata.ru/api/geocode/"/>.
/// </summary>
public class Client : IRouteCalculator
{
    /// <summary>
    /// Place of Moscow City Police office point.
    /// </summary>
    private static readonly Point MoscowCityPolicePoint = new Point
    {
        Latitude = 55.7697083F,
        Longitude = 37.6123129F
    };

    /// <summary>
    /// <see cref="TomTomUriBuilder"/>.
    /// </summary>
    private readonly TomTomUriBuilder _uriBuilder;

    /// <summary>
    /// Implementation of <see cref="ICleanClientAsync"/>.
    /// </summary>
    private readonly ICleanClientAsync _cleanClient;

    /// <summary>
    /// Base HTTP client.
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Create instance of <see cref="Client"/>.
    /// </summary>
    /// <param name="uriBuilder"><see cref="TomTomUriBuilder"/>.</param>
    /// <param name="cleanClient">Implementation of <see cref="ICleanClientAsync"/>.</param>
    public Client([NotNull] TomTomUriBuilder uriBuilder, [NotNull] ICleanClientAsync cleanClient)
    {
        _uriBuilder = uriBuilder ?? throw new ArgumentNullException(nameof(uriBuilder));
        _cleanClient = cleanClient ?? throw new ArgumentNullException(nameof(cleanClient));
        _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(60) };
    }

    /// <inheritdoc />
    public async Task<int> CalculateRouteAsync(string city, string address)
    {
        var targetPoint = await GetTargetPointAsync(city, address).ConfigureAwait(false);
        var json = await RequestTomTomAsync(targetPoint).ConfigureAwait(false);
        try {

            var dto = JsonConvert.DeserializeObject<RoutingResponseDto>(json) ?? throw new TomTomException();
            return dto.Routes.Summary.TravelTimeInSeconds;
        }
        catch {
            throw new TomTomException();
        }
    }

    /// <summary>
    /// Get TomTom service request content.
    /// </summary>
    /// <param name="target"><see cref="Point"/> for target address.</param>
    /// <returns>JSON content response.</returns>
    /// <exception cref="TomTomException"><see cref="TomTomException"/>.</exception>
    private async Task<string> RequestTomTomAsync(Point target)
    {
        var requestUri = _uriBuilder.Build(MoscowCityPolicePoint, target);
        try {
            var response = await _httpClient.GetAsync(requestUri).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
                throw new TomTomException();

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        }
        catch {
            throw new TomTomException();
        }
    }

    /// <summary>
    /// Getting location of target point.
    /// </summary>
    /// <param name="city">User town.</param>
    /// <param name="address">User city.</param>
    /// <returns><see cref="Point"/> for target address.</returns>
    /// <exception cref="DaDataNotFoundException"><see cref="DaDataNotFoundException"/>.</exception>
    private async Task<Point> GetTargetPointAsync(string city, string address)
    {
        try {
            var cleanResult = await _cleanClient.Clean<Address>($"{city} {address}").ConfigureAwait(false);
            if (cleanResult == null)
                throw new DaDataNotFoundException();

            switch (cleanResult.qc_geo)
            {
                // Accuracy level: town. 
                case "3":
                    throw new DaDataNotFoundException();

                // Accuracy level: city. 
                case "4":
                    throw new DaDataNotFoundException();

                // Accuracy level: not defined. 
                case "5":
                    throw new DaDataNotFoundException();
            }

            float latitude, longitude;
            if (float.TryParse(cleanResult.geo_lat, out latitude) && float.TryParse(cleanResult.geo_lon, out longitude))
                return new Point { Latitude = latitude, Longitude = longitude };

            throw new DaDataNotFoundException();
        }
        catch {
            throw new DaDataNotFoundException();
        }
    }
}