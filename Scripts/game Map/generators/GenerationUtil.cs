namespace Atomation.GameMap;

using Godot;

/// <summary>
/// defines a bunch of utility functions meant to be use during world 
/// generation
/// </summary>
public static class GenerationUtil
{
    public static float[,] GenerateTemperatureMap(Vector2I size, float[,] heightMap, float[,] heatMap, MapSettings settings)
    {
        float[,] temperatureMap = new float[size.X, size.Y];
        if (settings.temperatureHeight == 0)
        {
            settings.temperatureHeight = 1;
        }

        for (int x = 0; x < size.X; x++)
        {
            for (int y = 0; y < size.Y; y++)
            {
                float temperature = heatMap[x, y] * -1 * settings.equatorBias;
                temperature -= (heightMap[x, y] / settings.temperatureHeight * settings.temperatureHeightLoss) + settings.baseTemperature;

                temperatureMap[x, y] = Mathf.InverseLerp(-1, 1, temperature); //insure value is between -1 and 1
            }
        }

        return temperatureMap;
    }

    public static float[,] GenerateMoisture(Vector2I size, float[,] heightMap, float[,] heatMap, MapSettings settings)
    {
        float[,] moistureMap = new float[size.X, size.Y];

        for (int x = 0; x < size.X; x++)
        {
            for (int y = 0; y < size.Y; y++)
            { 

            }
        }
        //todo
        return moistureMap;
    }

}