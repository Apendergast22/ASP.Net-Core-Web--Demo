using Newtonsoft.Json;

namespace CriminalCheckerBackend.Model.DTO.TomTom;

/// <summary>
/// Response route calculation DTO
/// </summary>
public class RoutingResponseDto
{
    /// <summary>
    /// Get or set JSON structure version.
    /// </summary>
    [JsonProperty("formatVersion")]
    public string FormatVersion { get; set; } = null!;

    /// <summary>
    /// Get or set collection of <see cref="Routes"/>.
    /// </summary>
    [JsonProperty("routes")]
    public ICollection<Routes> Routes { get; set; } = null!;
}