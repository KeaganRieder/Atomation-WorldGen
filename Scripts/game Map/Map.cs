namespace Atomation.GameMap;

using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// The Games map
/// </summary>
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

    public Map()
    {
        chunkHandler = new ChunkHandler();
        chunkLoader = new ChunkLoader(chunkHandler);

        settings = new MapSettings();
        settings.DefaultSettings();
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
        Vector2I size = settings.worldSize;
        Vector2I offset = Vector2I.Zero;
        GradientMapGenerator GradientMapGenerator = new GradientMapGenerator();
        NoiseMapGenerator NoiseMapGenerator = new NoiseMapGenerator(settings.elevationMapConfigs);

        float[,] heightMap = NoiseMapGenerator.Run(offset, size);
        float[,] temperatureMap = GradientMapGenerator.Run(offset, size, size, settings.trueCenter);

        temperatureMap = GenerationUtil.GenerateTemperatureMap(size, temperatureMap, heightMap, settings);

        NoiseMapGenerator = new NoiseMapGenerator(settings.rainfallMapConfigs);
        float[,] rainfallMap = NoiseMapGenerator.Run(offset, size);

        float[,] moistureMap = GenerationUtil.GenerateMoisture(size, heightMap, rainfallMap, temperatureMap, settings);

        GenStepTerrain genStepTerrain = new GenStepTerrain(heightMap, temperatureMap, moistureMap);

        Tile[,] terrain = genStepTerrain.Run(offset, size);

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
        // GenerateNoiseMaps(default, settings.WorldSize, chunkPosition);
    }

}