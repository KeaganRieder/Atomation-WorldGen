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
        worldSize = new Vector2I(156, 156);
        elevationSettings = new GeneratorConfigs() //leave this alone!
        {
            Layered = false,
            BaseValue = 0,
            NoiseConfigs = new NoiseConfigs
            {
                Seed = 0,
                Octaves = 4,
                Frequency = 0.5f,
                Lacunarity = 3f,
                Gain = 0.41f,
                NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex,
                FractalType = FastNoiseLite.FractalTypeEnum.Fbm,
                NoiseOffset = Vector2.Zero,
            }
        };


        temperatureSettings = new GeneratorConfigs() //leave this alone!
        {
            Layered = true,
            TrueCenter = true,
            BaseValue = -0.1f,
            NoiseConfigs = null,
            LayerConfigs = new System.Collections.Generic.Dictionary<string, GenLayerConfigs>{
                {"Elevation", new GenLayerConfigs{LayerEffect = 0.5f, LayerHeight = 0.1f}}
            }
        };

        moistureGenSettings = new GeneratorConfigs
        {
            Layered = true,
            BaseValue = 0,
            NoiseConfigs = new NoiseConfigs
            {
                Seed = 0,
                Octaves = 5,
                Frequency = 0.03f,
                Lacunarity = 3.0f,
                Gain = 0.4f,
                NoiseType = FastNoiseLite.NoiseTypeEnum.Perlin,
                FractalType = FastNoiseLite.FractalTypeEnum.Fbm,
                NoiseOffset = Vector2.Zero,
            },
            LayerConfigs = new System.Collections.Generic.Dictionary<string, GenLayerConfigs>{
                {"Elevation", new GenLayerConfigs{LayerEffect = 0.1f, LayerHeight = 0.0f}},
                {"Temperature", new GenLayerConfigs{LayerEffect = 0.5f, LayerHeight = 0.0f}}

            }
        };
    }

}