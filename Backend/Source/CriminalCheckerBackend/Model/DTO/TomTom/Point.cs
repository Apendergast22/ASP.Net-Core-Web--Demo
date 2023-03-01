using Newtonsoft.Json;

namespace CriminalCheckerBackend.Model.DTO.TomTom;

/// <summary>
/// Rout point
/// </summary>
public class Point
{
    /// <summary>
    /// Get or set route latitude
    /// </summary>
    [JsonProperty("latitude")]
    public float Latitude { get; set; }

    /// <summary>
    /// Get or set route longitude
    /// </summary>
    [JsonProperty("longitude")]
    public float Longitude { get; set; }
}