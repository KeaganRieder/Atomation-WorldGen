namespace Atomation.GameMap;

using Godot;
using System;
using System.Collections.Generic;


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
    private LandScapeGenStep landScapeGenStep;

    public Map()
    {
        chunkHandler = new ChunkHandler();
        chunkLoader = new ChunkLoader(chunkHandler);

        settings = new MapSettings();
        settings.DefaultSettings();

        elevationGenerator = new NoiseGenerator();
        temperatureGenerator = new TemperatureGenerator();
        moistureGenerator = new MoistureGenerator();
        landScapeGenStep = new LandScapeGenStep();
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
        Dictionary<string, float[,]> noiseMaps = GenerateNoiseMaps(settings.WorldSize, settings.WorldSize, Vector2I.Zero);

        landScapeGenStep.SetNoiseMaps(noiseMaps);
        landScapeGenStep.SetTotalSize(settings.WorldSize);
        Tile[,] terrain = landScapeGenStep.Run(Vector2I.Zero, settings.WorldSize);

        foreach (var tile in terrain)
        {
            AddChild(tile);
        }
    }

    /// <summary>
    /// generates a chunk at given position
    /// </summary>
    public void GenerateChunk(Vector2 chunkPosition)
    {
        GenerateNoiseMaps(default, settings.WorldSize, chunkPosition);
    }

    /// <summary>
    /// generates the moisture, elevation and temperature maps
    /// which then is passed to generationSteps
    /// </summary>
    private Dictionary<string, float[,]> GenerateNoiseMaps(Vector2I size, Vector2I TotalSize, Vector2 offset)
    {
        Dictionary<string, float[,]> noiseMaps = new Dictionary<string, float[,]>();

        elevationGenerator.SetSettings(settings.ElevationSettings);
        temperatureGenerator.SetSettings(settings.TemperatureSettings);
        moistureGenerator.SetSettings(settings.MoistureGenSettings);

        elevationGenerator.SetTotalSize(TotalSize);
        noiseMaps.Add("elevation", elevationGenerator.Run(offset, size));

        GD.Print("Temp");
        NoiseGenerator heatMap = new NoiseGenerator();
        heatMap.SetTotalSize(TotalSize);
        heatMap.SetSettings(settings.TemperatureSettings);

        temperatureGenerator.SetTotalSize(TotalSize);
        temperatureGenerator.HeatMap = new LayerConfig
        {
            LayerEffect = 1f,
            ValueMap = heatMap.Run(offset + new Vector2(100,100), size),
            Squared = true,
            Positive = false
        };

        noiseMaps.Add("temperature", temperatureGenerator.Run(offset, size));

        GD.Print("moist");
        NoiseGenerator rainFallMap = new NoiseGenerator();
        rainFallMap.SetTotalSize(TotalSize);
        rainFallMap.SetSettings(settings.MoistureGenSettings);

        moistureGenerator.Elevation = new LayerConfig
        {
            LayerEffect = 0.8f,
            ValueMap = noiseMaps["elevation"],
            Squared = true,
            Positive = false
        };
        moistureGenerator.Temperature = new LayerConfig
        {
            LayerEffect = 0.1f,
            ValueMap = noiseMaps["temperature"],
            Squared = true,
            Positive = false
        };
        moistureGenerator.Rainfall = new LayerConfig
        {
            LayerEffect = 0.5f,
            ValueMap = rainFallMap.Run(offset + new Vector2(100, 100), size),
            Squared = false,
            Positive = true
        };

        moistureGenerator.SetTotalSize(TotalSize);
        noiseMaps.Add("moisture", moistureGenerator.Run(offset, size));

        return noiseMaps;
    }

}