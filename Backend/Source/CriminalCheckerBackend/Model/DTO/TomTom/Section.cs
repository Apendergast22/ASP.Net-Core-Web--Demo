using Newtonsoft.Json;

namespace CriminalCheckerBackend.Model.DTO.TomTom;

/// <summary>
/// Response summary info.
/// *There is more information https://developer.tomtom.com/routing-api/documentation/routing/calculate-route
/// </summary>
public class Section
{
    /// <summary>
    /// Get or set index of the first point (offset 0) in the route this section.*
    /// </summary>
    [JsonProperty("startPointIndex")]
    public int StartPointIndex { get; set; }

    /// <summary>
    /// Get or set index of the last point (offset 0) in the route this section.*
    /// </summary>
    [JsonProperty("endPointIndex")]
    public int EndPointIndex { get; set; }

    /// <summary>
    /// Get or ser request mode.*
    /// </summary>
    [JsonProperty("sectionType")]
    public string SectionType { get; set; } = null!;

    /// <summary>
    /// Get or ser travel mode.*
    /// </summary>
    [JsonProperty("travelMode")]
    public string TravelMode { get; set; } = null!;
}