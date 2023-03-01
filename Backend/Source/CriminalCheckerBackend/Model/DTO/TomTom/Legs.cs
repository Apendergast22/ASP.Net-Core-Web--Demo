using Newtonsoft.Json;

namespace CriminalCheckerBackend.Model.DTO.TomTom;

/// <summary>
/// Model with routes.
/// </summary>
public class Legs
{
    /// <summary>
    /// Get or set <see cref="TomTom.Summary"/>
    /// </summary>
    [JsonProperty("summary")]
    public Summary Summary { get; set; } = null!;

    /// <summary>
    /// Get or set collection of <see cref="Point"/>
    /// </summary>
    [JsonProperty("points")]
    public IEnumerable<Point> Points { get; set; } = null!;
}