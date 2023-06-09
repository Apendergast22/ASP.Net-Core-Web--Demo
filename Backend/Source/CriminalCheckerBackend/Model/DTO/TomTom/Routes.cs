﻿using Newtonsoft.Json;

namespace CriminalCheckerBackend.Model.DTO.TomTom;

/// <summary>
/// Model with main routes info
/// </summary>
public class Routes
{
    /// <summary>
    /// Get or set <see cref="TomTom.Summary"/>.
    /// </summary>
    [JsonProperty("summary")]
    public Summary Summary { get; set; } = null!;

    /// <summary>
    /// Get or set collection of <see cref="TomTom.Legs"/>.
    /// </summary>
    [JsonProperty("legs")]
    public ICollection<Legs> Legs { get; set; } = null!;

    /// <summary>
    /// Get or set collection of <see cref="Section"/>.
    /// </summary>
    [JsonProperty("sections")]
    public ICollection<Section> Sections { get; set; } = null!;
}