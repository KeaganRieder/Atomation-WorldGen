namespace Atomation.GameMap;

using Godot;
using System;

public partial class Map : Node2D
{
    public const int CELL_SIZE = 16;

    private static Map instance;
    public static Map Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Map();
            }
            return instance;
        }
    }

    private MapSettings settings;

    private ChunkHandler chunkHandler;
    private ChunkLoader chunkLoader;

    private NoiseGenerator elevationGenerator;
    private TemperatureGenerator temperatureGenerator;
    private MoistureGenerator moistureGenerator;

    private LandscapeGenerator landScapeGenerator;

    public Map()
    {
        chunkHandler = new ChunkHandler();
        chunkLoader = new ChunkLoader(chunkHandler); //todo

        settings = new MapSettings();
        settings.DefaultSettings();

        elevationGenerator = new NoiseGenerator();
        temperatureGenerator = new TemperatureGenerator();
        moistureGenerator = new MoistureGenerator();

        landScapeGenerator = new LandscapeGenerator();
    }

    public override void _Ready()
    {
        base._Ready();
        GenerateMap();
    }

    public override void _Process(double delta)
    {
        chunkLoader.TryLoading();
    }

    /// <summary> 
    /// clears the map 
    /// </summary>
    public void ClearMap()
    {
        chunkHandler.Clear();
    }

    /// <summary>
    /// generate the map 
    /// </summary>
    public void GenerateMap()
    {
        Vector2I size = settings.WorldSize;
        Vector2I offset = Vector2I.Zero;

        elevationGenerator.SetSettings(settings.ElevationSettings);
        elevationGenerator.SetTotalSize(settings.WorldSize);
        elevationGenerator.RunGenerator(offset, size);

        settings.TemperatureSettings.SetLayerMap("Elevation", elevationGenerator);
        settings.MoistureGenSettings.SetLayerMap("Elevation", elevationGenerator);

        temperatureGenerator.SetSettings(settings.TemperatureSettings);
        temperatureGenerator.SetTotalSize(settings.WorldSize);
        temperatureGenerator.RunGenerator(offset, size);

        settings.MoistureGenSettings.SetLayerMap("Temperature", temperatureGenerator);
        moistureGenerator.SetTotalSize(settings.WorldSize);
        moistureGenerator.SetSettings(settings.MoistureGenSettings);
        moistureGenerator.RunGenerator(offset, size);

        landScapeGenerator.SetElevation(elevationGenerator.GetMap());
        landScapeGenerator.SetTemperature(temperatureGenerator.GetMap());
        landScapeGenerator.SetMoisture(moistureGenerator.GetMap());

        landScapeGenerator.RunGenerator(offset, size);

        Tile[,] tiles = landScapeGenerator.GetMap();

        for (int x = 0; x < size.X; x++)
        {
            for (int y = 0; y < size.Y; y++)
            {
                AddChild(tiles[x, y]);
            }
        }

        elevationGenerator.ClearMap();
        temperatureGenerator.ClearMap();
        landScapeGenerator.ClearMap();
    }

    /// <summary>
    /// generates a chunk at given position
    /// </summary>
    public void GenerateChunk(Vector2 chunkPosition)
    {
    }
}