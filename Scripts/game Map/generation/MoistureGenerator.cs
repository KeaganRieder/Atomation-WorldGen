namespace Atomation.GameMap;

using Godot;

public class MoistureGenerator : NoiseGenerator
{
    private GenLayerConfigs elevationLayer;
    private GenLayerConfigs TemperatureLayer;

    private Vector2I rainfallMapOffset;

    public MoistureGenerator(GeneratorConfigs moistureGenSettings = null) : base(moistureGenSettings)
    {
        rainfallMapOffset = new Vector2I(100, 100);
    }

    public override void RunGenerator(Vector2 offset = default, Vector2I size = default)
    {
        if (!ValidateGenerator())
        {
            return;
        }
        elevationLayer = settings.LayerConfigs["Elevation"];
        TemperatureLayer = settings.LayerConfigs["Temperature"];

        SetSize(size);
        SetOffset(offset);
        settings.NoiseConfigs.NoiseOffset = genOffset;

        FastNoiseLite fastNoiseLite = settings.NoiseConfigs.FastNoiseLite;

        for (int x = 0; x < genSize.X; x++)
        {
            for (int y = 0; y < genSize.Y; y++)
            {
                float sampleX = (x + rainfallMapOffset.X) / totalSize.X;
                float sampleY = (y + rainfallMapOffset.Y) / totalSize.Y;

                float rainFall = Mathf.InverseLerp(-1, 1, fastNoiseLite.GetNoise2D(sampleX, sampleY));

                float moisture = rainFall - elevationLayer.GetLayerValue(x, y);
                moisture -= TemperatureLayer.GetLayerValue(x, y);

                moisture += settings.BaseValue;

                noiseMap[x, y] = moisture;
                SetMin(noiseMap[x, y]);
                SetMax(noiseMap[x, y]);
            }
        }
        PrintMinMax();
    }

}