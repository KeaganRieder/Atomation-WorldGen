namespace Atomation.GameMap;

using Godot;
using Newtonsoft.Json;

/// <summary>
/// defines settings for configuring how the world map is generated
/// </summary>
public class MapSettings
{
    // general world settings
    public Vector2I worldSize { get; set; }

    public bool infiniteWorld { get; set; }

    [JsonIgnore]
    public bool trueCenter { get; set; }

    [JsonIgnore]
    public NoiseMapConfigs elevationMapConfigs { get; set; }

    /// <summary>
    /// highest temperature at the equator, used to offset temperature
    /// to be closer to it
    /// </summary>
    public float baseTemperature { get; set; }
    /// <summary>
    /// value used to determine how big of a decrease a value has the 
    /// further awy form the center it is
    /// </summary>
    public float equatorBias { get; set; }
    /// <summary>
    /// incremental step at which temperature should be lost
    /// </summary>
    public float temperatureHeight { get; set; }
    /// <summary>
    /// the amount of temperature that should be lost 
    /// for each height step
    /// </summary>
    public float temperatureHeightLoss { get; set; }

    public MapSettings() { }

    /// <summary>
    /// sets maps settings to default
    /// </summary>
    public void DefaultSettings()
    {
        worldSize = new Vector2I(100, 100);

        infiniteWorld = true;
        trueCenter = true;

        elevationMapConfigs = new NoiseMapConfigs
        {
            Scale = 90,
            Seed = 0,
            Octaves = 5,
            Frequency = 3f,
            Lacunarity = 3f,
            Gain = 0.4f,
            NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex,
            FractalType = FastNoiseLite.FractalTypeEnum.Fbm,
            NoiseOffset = Vector2.Zero,
            Normalized = false,
        };

        baseTemperature = .5f;
        equatorBias = 1.5f;
        temperatureHeight = .2f;
        temperatureHeightLoss = .5f;
    }

}