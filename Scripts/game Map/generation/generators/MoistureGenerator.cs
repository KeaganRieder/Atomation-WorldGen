namespace Atomation.GameMap;

using Godot;

public class MoistureGenerator : NoiseGenerator
{
    public LayerConfig Elevation { get; set; }
    public LayerConfig Temperature { get; set; }
    public LayerConfig Rainfall { get; set; }

    public MoistureGenerator(GeneratorConfigs moistureGenSettings = null) : base(moistureGenSettings) { }

    public override float[,] Run(Vector2 offset = default, Vector2I size = default)
    {
        if (!Validate())
        {
            return default;
        }

        SetSize(size);
        SetOffset(offset);
        settings.NoiseConfigs.NoiseOffset = genOffset;
        float[,] noiseMap = new float[genSize.X, genSize.Y];

        for (int x = 0; x < genSize.X; x++)
        {
            for (int y = 0; y < genSize.Y; y++)
            {
                float layerMoisture = Rainfall.GetValue(x, y) + Elevation.GetValue(x, y) + Temperature.GetValue(x, y);
                float moisture = settings.BaseValue + layerMoisture;

                noiseMap[x, y] = moisture;
                SetMin(noiseMap[x, y]);
                SetMax(noiseMap[x, y]);
            }
        }
        PrintMinMax();
        return noiseMap;
    }

}