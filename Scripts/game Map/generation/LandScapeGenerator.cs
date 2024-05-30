namespace Atomation.GameMap;

using Godot;

/// <summary>
/// defines functions which use noise maps to generate the
/// maps landscape
/// </summary>
public class LandscapeGenerator : Generator<Tile>
{
    private float[,] elevationMap;
    private float[,] temperatureMap;
    private float[,] moistureMap;

    private Tile[,] generatedTiles;

    public override void SetSize(Vector2I size)
    {
        base.SetSize(size);
        generatedTiles = new Tile[genSize.X, genSize.Y];
    }

    /// <summary>
    /// sets the noise map which relate to the maps elevation
    /// </summary>
    public void SetElevation(float[,] noiseMap)
    {
        elevationMap = noiseMap;
    }
    /// <summary>
    /// sets the noise map which relate to the maps temperature
    /// </summary>
    public void SetTemperature(float[,] noiseMap)
    {
        temperatureMap = noiseMap;
    }
    /// <summary>
    /// sets the noise map which relate to the maps moisture
    /// </summary>
    public void SetMoisture(float[,] noiseMap)
    {
        moistureMap = noiseMap;
    }

    /// <summary>
    /// runs generator 
    /// </summary>
    public override void RunGenerator(Vector2 offset = default, Vector2I size = default)
    {
        if (elevationMap == null)
        {
            GD.PushError("can't generate noiseMaps aren't provided are not provided");
            generatedTiles = null;
            return;
        }

        SetSize(size);
        SetOffset(offset);

        for (int x = 0; x < genSize.X; x++)
        {
            for (int y = 0; y < genSize.Y; y++)
            {
                Vector2 tilePosition = new Vector2(x + offset.X, y + offset.Y) * Map.CELL_SIZE;
                Tile tile = new Tile(tilePosition);

                // float[,] colorVal = temperatureMap;
                // float[,] colorVal = elevationMap;
                float[,] colorVal = moistureMap;
                    
                tile.SetColor(GetTileColor(colorVal[x, y],true));


                generatedTiles[x, y] = tile;
            }
        }

        elevationMap = null;
        temperatureMap = null;
        moistureMap = null;
    }

    /// <summary> 
    /// returns a value at the given point form noise map generated form the generator
    /// </summary>
    public override Tile GetValue(int x, int y)
    {
        return generatedTiles[x, y];
    }

    /// <summary>
    /// returns a map of values which are the result form the generator
    /// </summary>
    public override Tile[,] GetMap()
    {
        return generatedTiles;
    }

    public override void ClearMap()
    {
        generatedTiles = null;
    }

    private Color GetTileColor(float value, bool blackAndWhite)
    {
        if (blackAndWhite)
        {
            return new Color(value, value, value);
        }

        return Colors.Black;
    }
    private Color GetTemperatureColor(float Temperature)
    {
        Color heatColor;

        if (Temperature < -1.3)
        {
            heatColor = Colors.White;
        }
        else if (Temperature < -1.0)
        {
            heatColor = Colors.Pink;
        }
        else if (Temperature < -0.8)
        {
            heatColor = Colors.Purple;
        }
        else if (Temperature < -0.5)
        {
            heatColor = Colors.DarkBlue;
        }
        else if (Temperature < -0.25)
        {
            heatColor = Colors.Blue;
        }
        else if (Temperature < -0.10)
        {
            heatColor = Colors.LightBlue;
        }
        else if (Temperature < 0.25)
        {
            heatColor = Colors.ForestGreen;
        }
        else if (Temperature < 0.7)
        {
            heatColor = Colors.DarkGreen;
        }
        else if (Temperature < 1)
        {
            heatColor = Colors.Yellow;
        }
        else if (Temperature < 1.25)
        {
            heatColor = Colors.Orange;
        }
        else if (Temperature < 1.7)
        {
            heatColor = Colors.Red;
        }
        else
        {
            heatColor = Colors.DarkRed;
        }
        
        return heatColor;
    }
      private Color GetElevationColor(float elevation)
    {
        Color heatColor;

        if (elevation <= 0.15)
        {
            heatColor = Colors.DarkBlue;
        }
        else if (elevation <= 0.3)
        {
            heatColor = Colors.Cyan;
        }
        else if (elevation <= 0.5)
        {
            heatColor = Colors.DarkGray;
        }
        else if (elevation <= 0.7)
        {
            heatColor = Colors.Gray;
        }
        else if (elevation <= 0.8)
        {
            heatColor = Colors.LightGray;
        }
       
        else
        {
            heatColor = Colors.White;
        }
        
        return heatColor;
    }
}