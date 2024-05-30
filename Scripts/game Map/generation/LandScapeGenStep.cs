namespace Atomation.GameMap;

using System.Collections.Generic;
using Godot;

/// <summary>
/// This is the first generation step when it comes to creating the world
/// this step handles creating the maps landscape
/// </summary>
public class LandScapeGenStep : Generator<Tile>
{
    private float[,] elevation;
    private float[,] temperature; //temperature is generally between -.2 -.8
    private float[,] moisture; // moisture is normally between -.2 and .8

    private float hotClimate = 0.7f;
    private float warmTemperateClimate = 0.5f;
    private float temperateClimate = 0.3f;

    private float waterLevel = -0.1f;
    private float mountainSize = 0.4f;

    private float coldest = -0.6f;
    private float cold = -0.3f;
    private float temperate = -0.0f;
    private float warm = .2f;
    private float hot = .4f;
    private float hotest = .6f;


    public LandScapeGenStep()
    {
        elevation = null;
        temperature = null;
        moisture = null;
    }

    public void SetNoiseMaps(Dictionary<string, float[,]> noiseMaps)
    {
        elevation = noiseMaps["elevation"];
        temperature = noiseMaps["temperature"];
        moisture = noiseMaps["moisture"];
    }

    public override void SetSize(Vector2I size = default)
    {
        base.SetSize(size);
    }

    protected override bool Validate()
    {

        if (elevation == null || temperature == null || moisture == null)
        {
            GD.PushError("Can't Generate landscape for required layers haven't been set");
            return false;
        }
        return true;
    }

    public override Tile[,] Run(Vector2 offset = default, Vector2I size = default)
    {
        if (!Validate())
        {
            return default;
        }

        SetSize(size);
        SetOffset(offset);
        Tile[,] terrain = new Tile[genSize.X, genSize.Y];

        for (int x = 0; x < genSize.X; x++)
        {
            for (int y = 0; y < genSize.Y; y++)
            {
                Vector2 tilePosition = new Vector2(x + offset.X, y + offset.Y) * Map.CELL_SIZE;
                Tile tile = new Tile(tilePosition);

                // string biome = SelectBiome(x, y);

                // tile.SetColor(FormLandScape(x, y));
                // tile.SetColor(new Color(temperature[x,y],temperature[x,y],temperature[x,y]));

                tile.SetColor(GetTemperature(x, y));

                // tile.SetColor(GetBiomeColor(biome));

                terrain[x, y] = tile;
            }
        }

        return terrain;
    }

    private Color FormLandScape(int x, int y)
    {
        //mountain
        if (elevation[x, y] > mountainSize)
        {
            if (elevation[x, y] > mountainSize + .1)
            {
                return Colors.White;

            }
            return Colors.LightGray;
        }
        //land
        if (elevation[x, y] > waterLevel)
        {
            if (elevation[x, y] > waterLevel + .1)
            {
                return Colors.Gray;

            }
            return Colors.DarkGray;
        }
        //water
        return Colors.Black;
    }
    // -0.3 and 1.5
    private Color GetTemperature(int x, int y)
    {

        if (temperature[x, y] > hotest)
        {
            return Colors.Red;
        }
        if (temperature[x, y] > hot)
        {
            return Colors.Orange;
        }
         if (temperature[x, y] > warm)
        {
            return Colors.Yellow;
        }
        if (temperature[x, y] > temperate)
        {
            return Colors.Green;
        }
        if (temperature[x, y] > cold)
        {
            return Colors.Blue;
        }
        if (temperature[x, y] > coldest)
        {
            return Colors.DarkBlue;
        }

        return Colors.Purple;
    }

    private Color GetBiomeColor(string biome)
    {
        GD.Print(biome);
        switch (biome)
        {
            case "Rainforest": return new Color(0.0f, 0.6f, 0.0f); // Dark Green
            case "Savanna": return new Color(0.9f, 0.9f, 0.2f); // Yellow
            case "Desert": return new Color(1.0f, 0.9f, 0.5f); // Sand
            case "Seasonal Forest": return new Color(0.2f, 0.7f, 0.2f); // Medium Green
            case "Grassland": return new Color(0.5f, 1.0f, 0.5f); // Light Green
            case "DryGrassLand": return new Color(0.5f, 1.0f, 0.25f); // Light Green
            case "Temperate Rainforest": return new Color(0.0f, 0.5f, 0.0f); // Darker Green
            case "Temperate Forest": return new Color(0.3f, 0.6f, 0.3f); // Green
            case "Shrubland": return new Color(0.7f, 0.7f, 0.3f); // Olive
            case "Taiga": return new Color(0.3f, 0.4f, 0.3f); // Gray Green
            case "Tundra": return new Color(0.8f, 0.8f, 1.0f); // Light Blue
            default: return new Color(1.0f, 1.0f, 1.0f); // White
        }
    }

    /// <summary>
    /// selects the cells biome and terrain type based on
    /// the cells temperature and moisture
    /// </summary>
    private string SelectBiome(int x, int y)
    {
        /// moisture is normally between -.2 and .8
        if (temperature[x, y] > hotClimate)
        {
            if (moisture[x, y] > 0.7)
            {
                return "RainForest";
            }
            else if (moisture[x, y] > 0.4)
            {
                return "Savanna";
            }
            return "Desert";
        }
        else if (temperature[x, y] > warmTemperateClimate)
        {
            if (moisture[x, y] > 0.7)
            {
                return "SeasonalForest";
            }
            else if (moisture[x, y] > 0.4)
            {
                return "GrassLand";
            }
            return "Desert";
        }
        else if (temperature[x, y] > temperateClimate)
        {
            if (moisture[x, y] > 0.7)
            {
                return "TemperateRainForest";
            }
            else if (moisture[x, y] > 0.4)
            {
                return "TemperateForest";
            }
            return "ShrubLand";
        }
        else
        {
            if (moisture[x, y] > 0.6)
            {
                return "Taiga";
            }
            return "Tundra";
        }
    }

}