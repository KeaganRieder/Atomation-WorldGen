namespace Atomation.GameMap;

using Godot;
using Newtonsoft.Json;

/// <summary>
/// defines settings for the games map
/// </summary>
public class MapSettings
{
    [JsonProperty]
    private Vector2I worldSize;
    [JsonProperty]
    private bool infiniteWorld;


    [JsonProperty]
    private GeneratorConfigs elevationSettings;

    [JsonProperty]
    private GeneratorConfigs temperatureSettings;


    [JsonProperty]
    private GeneratorConfigs moistureGenSettings;

    public MapSettings() { }

    [JsonIgnore]
    public Vector2I WorldSize { get => worldSize; set => worldSize = value; }
    [JsonIgnore]
    public bool InfiniteWorld { get => infiniteWorld; set => infiniteWorld = value; }
    [JsonIgnore]
    public GeneratorConfigs ElevationSettings { get => elevationSettings; set => elevationSettings = value; }
    [JsonIgnore]
    public GeneratorConfigs TemperatureSettings { get => temperatureSettings; set => temperatureSettings = value; }
    [JsonIgnore]
    public GeneratorConfigs MoistureGenSettings { get => moistureGenSettings; set => moistureGenSettings = value; }

    public void DefaultSettings()
    {
        InfiniteWorld = true;
        worldSize = new Vector2I(100, 100);

        elevationSettings = new GeneratorConfigs() 
        {
            BaseValue = 0,
            NoiseConfigs = new NoiseConfigs
            {
                Seed = 0,
                Octaves = 5,
                Frequency = 1f,
                Lacunarity = 4f,
                Gain = 0.2f,
                NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex,
                FractalType = FastNoiseLite.FractalTypeEnum.Fbm,
                NoiseOffset = Vector2.Zero,
                Normalized = false,
            }
        };

        temperatureSettings = new GeneratorConfigs()
        {
            TrueCenter = false,
            BaseValue = 0.0f,
            NoiseConfigs = new NoiseConfigs
            {
                Seed = 0,
                Octaves = 2,
                Frequency = 3f,
                Lacunarity = 3f,
                Gain = 0.3f,
                NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex,
                FractalType = FastNoiseLite.FractalTypeEnum.Fbm,
                NoiseOffset = Vector2.Zero,
                Normalized = false,
            }
        };

        moistureGenSettings = new GeneratorConfigs
        {
            BaseValue = 0,
            NoiseConfigs = new NoiseConfigs
            {
                Seed = 0,
                Octaves = 5,
                Frequency = 2.5f,
                Lacunarity = 3.0f,
                Gain = 0.4f,
                NoiseType = FastNoiseLite.NoiseTypeEnum.Perlin,
                FractalType = FastNoiseLite.FractalTypeEnum.Fbm,
                NoiseOffset = Vector2.Zero,
                Normalized = true,
            },

        };
    }

}