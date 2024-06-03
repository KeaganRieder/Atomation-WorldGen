namespace Atomation.GameMap;

using System.Collections.Generic;
using Godot;

/// <summary>
/// defines the step in generation which handles teh creation of the games
/// terrain
/// </summary>
public class GenStepTerrain : Generator<Tile>
{
    private float[,] elevation;
    private float[,] temperature; //temperature is generally between -.2 -.8
    private float[,] moisture; // moisture is normally between -.2 and .8

    private float hotClimate = 0.7f;
    private float warmTemperateClimate = 0.5f;
    private float temperateClimate = 0.3f;

    private float waterLevel = -0.2f;
    private float mountainSize = 0.4f;

    //temperature things
    private float coldest = -0.6f;
    private float cold = -0.3f;
    private float temperate = -0.0f;
    private float warm = .2f;
    private float hot = .4f;
    private float hottest = .6f;

    //moisture thing
    private float dryest = -0.47f;
    private float dryer = -0.25f;
    private float dry = 0.2f;
    private float wet = 0.4f;
    private float wetter = 0.6f;

    public GenStepTerrain(float[,] elevationMap = default, float[,] temperatureMap = default, float[,] moistureMap = default)
    {
        configure(elevationMap, temperatureMap, moistureMap);
    }

    public void configure(float[,] elevation = default, float[,] temperature = default, float[,] moisture = default)
    {
        this.elevation = elevation;
        this.temperature = temperature;
        this.moisture = moisture;
    }

    public override void SetSize(Vector2I size = default)
    {
        base.SetSize(size);
    }

    protected bool Validate()
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
        float min = 10;
        float max = -10;

        SetSize(size);
        SetOffset(offset);
        Tile[,] terrain = new Tile[size.X, size.Y];

        for (int x = 0; x < size.X; x++)
        {
            for (int y = 0; y < size.Y; y++)
            {
                Vector2 tilePosition = new Vector2(x + offset.X, y + offset.Y) * Map.CELL_SIZE;
                Tile tile = new Tile(tilePosition);

                if (moisture[x, y] < min)
                {
                    min = moisture[x, y];
                }
                if (moisture[x, y] > max)
                {
                    max = moisture[x, y];
                }

                // tile.SetColor(FormLandScape(x, y));
                // tile.SetColor(FromMoisture(x, y));
                // tile.SetColor(GetTemperature(x, y));

                tile.SetColor(SelectBiome(x, y));

                terrain[x, y] = tile;
            }
        }
        GD.Print($"Min: {min} Max:{max}");
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
    
    private Color GetTemperature(int x, int y)
    {

        if (temperature[x, y] > hottest)
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
    private Color FromMoisture(int x, int y)
    {
        if (moisture[x, y] < dryest)
        {
            return Colors.Orange;
        }
        else if (moisture[x, y] < dryer)
        {
            return Colors.Yellow;
        }
        else if (moisture[x, y] < dry)
        {
            return Colors.Green;
        }
        else if (moisture[x, y] < wet)
        {
            return Colors.Cyan;
        }
        else if (moisture[x, y] < wetter)
        {
            return Colors.Blue;
        }
        else
        {
            return Colors.DarkBlue;
        }
    }

    /// <summary>
    /// selects the cells biome and terrain type based on
    /// the cells temperature and moisture
    /// </summary>
    private Color SelectBiome(int x, int y)
    { 
        //temp:-.8 to .6 moisture:-.8 to .8
        // cold
        if (temperature[x, y] < -.15f)
        {
            if (moisture[x, y] > 0.0)
            {
                // taiga
                return new Color(0.3f, 0.4f, 0.3f);
            }
            else
            {
                // tundra
                return Colors.LightBlue;
            }
        }
        //temperate
        else if (temperature[x, y] < .25f)
        {
            if (moisture[x, y] > .4)
            {
                // Seasonal Forest
                return new Color(0.2f, 0.7f, 0.2f);
            }
            else if (moisture[x, y] > 0)
            {
                // grass land
                return Colors.LightGreen;
            }
            else
            {
                // shrub land
                return new Color(1.0f, 0.9f, 0.5f);
            }
        }
        // warmest
        else
        {
            if (moisture[x, y] > .6)
            {
                //rain forest
                return Colors.DarkGreen;
            }
            else if (moisture[x, y] > .3)
            {
                // temperate forest
                return new Color(0.5f, 1.0f, 0.5f);
            }
            else if (moisture[x, y] > 0.1)
            {
                // grass land
                return Colors.LightGreen; 
            }
            else if (moisture[x, y] > -.20)
            {
                // savanna
                return new Color(0.9f, 0.9f, 0.2f);
            }
            else
            {
                // dessert
                return new Color(0.7f, 0.7f, 0.3f);
            }
        }
    }
}