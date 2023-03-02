using CriminalCheckerBackend.Model.DTO.TomTom;
using System.Globalization;

namespace CriminalCheckerBackend.Services.TomTomApi;

/// <summary>
/// Request route calculation DTO
/// </summary>
public class TomTomUriBuilder
{
    /// <summary>
    /// API part (control).
    /// </summary>
    private const string Part = "routing";

    /// <summary>
    /// API method.
    /// </summary>
    private const string Method = "calculateRoute";

    /// <summary>
    /// API version.
    /// </summary>
    private const string Version = "1";

    /// <summary>
    /// Base API <see cref="Uri"/>.
    /// </summary>
    private readonly Uri _baseUri;

    /// <summary>
    /// API access token.
    /// </summary>
    private readonly string _token;

    /// <summary>
    /// Create new instance of <see cref="TomTomUriBuilder"/>.
    /// </summary>
    /// <param name="baseUri">Base API URI.</param>
    /// <param name="token">API access token.</param>
    public TomTomUriBuilder(string baseUri, string token)
    {
        _baseUri = new Uri(baseUri);
        _token = token;
    }

    /// <summary>
    /// Build request to API <see cref="Uri"/>.
    /// </summary>
    /// <param name="start">Start route point.</param>
    /// <param name="target">Target route point.</param>
    /// <returns>Result <see cref="Uri"/>.</returns>
    public Uri Build(Point start, Point target)
    {
        return new Uri(_baseUri, $"/{Part}/{Version}/{Method}/{PointToString(start)}:{PointToString(target)}/json?key={_token}");
    }

    /// <summary>
    /// Convert geographic point to string.
    /// </summary>
    /// <param name="point"><see cref="Part"/>.</param>
    /// <returns>Geographic point as string.</returns>
    private static string PointToString(Point point)
    {
        return string.Format(CultureInfo.InvariantCulture, "{0},{1}", point.Latitude, point.Longitude);
    }
}