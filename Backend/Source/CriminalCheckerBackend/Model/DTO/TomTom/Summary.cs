using Newtonsoft.Json;

namespace CriminalCheckerBackend.Model.DTO.TomTom;

/// <summary>
/// Summary routes information.
/// </summary>
public class Summary
{
    /// <summary>
    /// Get or set distance in meters.
    /// </summary>
    [JsonProperty("lengthInMeters")]
    public int LengthInMeters { get; set; }

    /// <summary>
    /// Get or set route time in seconds.
    /// </summary>
    [JsonProperty("travelTimeInSeconds")]
    public int TravelTimeInSeconds { get; set; }

    /// <summary>
    /// Get or set traffic delay in seconds.
    /// </summary>
    [JsonProperty("trafficDelayInSeconds")]
    public int TrafficDelayInSeconds { get; set; }

    /// <summary>
    /// Get or set departure time.
    /// </summary>
    [JsonProperty("departureTime")]
    public DateTimeOffset DepartureTime { get; set; }

    /// <summary>
    /// Get or set arrival time.
    /// </summary>
    [JsonProperty("arrivalTime")]
    public DateTimeOffset ArrivalTime { get; set; }
}