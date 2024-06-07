namespace Atomation.GameMap;

using Godot;

/// <summary>
/// defines the step in generation which handles teh creation of the games
/// terrain
/// </summary>
public class GenStepLandScape : Generator<object>
{
    private float[,] elevation;
    private float[,] temperature;
    private float[,] moisture;

    private float waterLevel = -0.23f;
    private float mountainSize = 0.45f;

    public GenStepLandScape(float[,] elevationMap = default, float[,] temperatureMap = default, float[,] moistureMap = default)
    {
        configure(elevationMap, temperatureMap, moistureMap);
    }

    public void configure(float[,] elevation = default, float[,] temperature = default, float[,] moisture = default)
    {
        this.elevation = elevation;
        this.temperature = temperature;
        this.moisture = moisture;
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

    public void Generate(out Tile[,] generatedTiles, Vector2 offset = default, Vector2I size = default)
    {
        if (!Validate())
        {
            GD.PushError("Required maps haven't been set, as such can't generate");
            generatedTiles = default;
            return;
        }

        SetSize(size);
        SetOffset(offset);

        generatedTiles = new Tile[genSize.X, genSize.Y];
        for (int x = 0; x < size.X; x++)
        {
            for (int y = 0; y < size.Y; y++)
            {
                Vector2 cord = new Vector2(x + offset.X, y + offset.Y) * Map.CELL_SIZE;
                Tile tile = new Tile(cord);
                tile.Moisture = moisture[x, y];
                tile.Elevation = elevation[x, y];
                tile.Temperature = temperature[x, y];

                tile.SetColor(FormLandScape(x, y));
                generatedTiles[x, y] = tile;
            }
        }
    }



    private Color FormLandScape(int x, int y)
    {
        //mountain
        if (elevation[x, y] > mountainSize)
        {
            if (elevation[x, y] > mountainSize + .1)
            {
                return Colors.DarkGray;

            }
            return Colors.Gray;
        }
        //land
        else if (elevation[x, y] > waterLevel)
        {
            if (elevation[x, y] > waterLevel + .1)
            {
                return SelectBiome(x, y);

            }
            return Colors.Black;
        }
        //water
        else
        {
            //deep water
            if (elevation[x, y] > waterLevel - 0.12)
            {
                return Colors.Blue;
            }
            else
            {
                return Colors.DarkBlue;

            }

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