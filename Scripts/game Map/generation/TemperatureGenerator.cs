namespace Atomation.GameMap;

using Godot;

/// <summary>
/// generates a temperature map using a gradient map and elevation map
/// </summary>
public class TemperatureGenerator : NoiseGenerator
{
    private float equatorHeight;
    private GenLayerConfigs elevationLayer;
    private float equatorTemp;
    private float poleTemp;

    // private TemperatureConfigs settings;

    public TemperatureGenerator() { }
    public TemperatureGenerator(GeneratorConfigs settings = null) : base(settings)
    {
    }

    public void SetSettings(float equatorTemp, float poleTemp, GeneratorConfigs settings)
    {
        base.SetSettings(settings);
        this.equatorTemp = equatorTemp;
        this.poleTemp = poleTemp;
    }
    public override void RunGenerator(Vector2 offset = default, Vector2I size = default)
    {
        if (!ValidateGenerator())
        {
            return;
        }

        elevationLayer = settings.LayerConfigs["Elevation"];

        SetSize(size);
        SetOffset(offset);
        equatorHeight = settings.TrueCenter ? totalSize.Y * .5f : 0;

        for (int y = 0; y < genSize.Y; y++)
        {
            for (int x = 0; x < genSize.X; x++)
            {
                noiseMap[x, y] = CalculateTemperature(x, y);
                SetMin(noiseMap[x, y]);
                SetMax(noiseMap[x, y]);
            }
        }
        PrintMinMax();
    }

    /// <summary>
    /// calculates temperature base on equator heat and elevation
    /// </summary>
    private float CalculateTemperature(int x, int y)
    {
        // float latitudeTemperature = 
        float equatorTemp = Mathf.Lerp(this.equatorTemp, poleTemp, DistanceFromEquator(y))*3;// + settings.BaseValue;

        float temperature = equatorTemp;//DistanceFromEquator(y);// equatorTemp ;//- elevationLayer.GetLayerValue(x, y);//settings.BaseValue + (latitudeTemperature );

        return temperature;// + settings.BaseValue;
    }

    /// <summary>
    /// calculates distance from the center to the given point
    /// </summary>
    private float DistanceFromEquator(int y)
    {
        float distance = Mathf.Abs((y + genOffset.Y) - equatorHeight);

        distance /= totalSize.Y;
        // GD.Print(distance);
        return distance;
    }
}