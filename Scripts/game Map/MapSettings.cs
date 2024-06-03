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

    public int seed { get; set; }

    [JsonIgnore]
    public bool trueCenter { get; set; }

    [JsonIgnore]
    public float noiseMapScale { get; set; }

    [JsonIgnore]
    public NoiseMapConfigs elevationMapConfigs { get; set; }
    [JsonIgnore]
    public NoiseMapConfigs rainfallMapConfigs { get; set; }

    public float baseMoisture { get; set; }

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
        trueCenter = false;

        noiseMapScale = 1;
        seed = 0;
        elevationMapConfigs = new NoiseMapConfigs
        {
            Scale = noiseMapScale,
            Seed = seed,
            Octaves = 6,
            Frequency = 0.01f,
            Lacunarity = 3f, 
            Gain = 0.4f,
            NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex,
            FractalType = FastNoiseLite.FractalTypeEnum.Fbm,
            NoiseOffset = Vector2.Zero,
            Normalized = false,
        };

       
        rainfallMapConfigs = new NoiseMapConfigs
        {
            Scale = noiseMapScale,
            Seed = seed,
            Octaves = 5,
            Frequency = 0.03f,
            Lacunarity = 2f, 
            Gain = 0.4f,
            NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex,
            FractalType = FastNoiseLite.FractalTypeEnum.Fbm,
            NoiseOffset = Vector2.Zero,
            Normalized = false,
        };

        baseMoisture = 0f;
        baseTemperature = 0.7f;
        // equatorBias = 2f;
        // temperatureHeight = .01f;
        // temperatureHeightLoss = .01f;
    }
}