namespace Atomation.GameMap;

using Godot;

/// <summary>
/// generates a temperature map using a gradient map and elevation map
/// </summary>
public class TemperatureGenerator : NoiseGenerator
{
    private float equatorHeight;
    public LayerConfig Elevation { get; set; }
    public LayerConfig HeatMap { get; set; }


    public TemperatureGenerator(GeneratorConfigs settings = null) : base(settings) { }

    public override float[,] Run(Vector2 offset = default, Vector2I size = default)
    {
        if (!Validate())
        {
            return default;
        }

        SetSize(size);
        SetOffset(offset);
        float[,] noiseMap = new float[genSize.X, genSize.Y];
        float[,] equatorHeat = new float[genSize.X, genSize.Y];

        equatorHeight = settings.TrueCenter ? totalSize.Y * .5f : 0;

        for (int y = 0; y < genSize.Y; y++)
        {
            for (int x = 0; x < genSize.X; x++)
            {
                equatorHeat[x, y] = 1 - CalculateEquatorTemperature(y);
                float temperature = equatorHeat[x, y] * HeatMap.GetValue(x, y);

                noiseMap[x, y] = temperature + settings.BaseValue + Elevation.GetValue(x,y);
                SetMin(noiseMap[x, y]);
                SetMax(noiseMap[x, y]);
            }
        }
        // //using equator heat to make temperature map
        // for (int x = 0; x < genSize.X; x++)
        // {
        //     for (int y = 0; y < genSize.Y; y++)
        //     {
                
        //     }
        // }

        PrintMinMax();
        return noiseMap;
    }

    /// <summary>
    /// calculates the equators temperature which is based on distance form it
    /// </summary>
    private float CalculateEquatorTemperature(int y)
    {
        float distance = Mathf.Abs(y + genOffset.Y - equatorHeight);

        distance /= totalSize.Y ;
        return distance;
    }
}