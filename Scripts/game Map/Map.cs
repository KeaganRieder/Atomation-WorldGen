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

    private MapGenerator mapGenerator;
    private ChunkHandler chunkHandler;
    private ChunkLoader chunkLoader;

    public Map()
    {
        settings = new MapSettings();

        mapGenerator = new MapGenerator(settings);
        chunkHandler = new ChunkHandler(this);
        chunkLoader = new ChunkLoader(chunkHandler);

        settings.DefaultSettings();

    }

    public override void _Ready()
    {
        base._Ready();
        // GeneratePreview();
        // AddChild(new Chunk(Vector2.Zero));
        chunkLoader.TryLoading();
    }

    public override void _Process(double delta)
    {
        // chunkLoader.TryLoading();
    }

    /// <summary> 
    /// clears the map 
    /// </summary>
    public void ClearMap()
    {
        //make delete kids  if in map preview
        GD.PushError("clearing not implemented");
        // chunkHandler.Clear();
    }

    public MapGenerator GetGenerator()
    {
        return mapGenerator;
    }

    /// <summary>
    /// generate the map 
    /// need to figure out a different way of doing this maybe something 
    /// like a map generator class
    /// </summary>
    public void GeneratePreview()
    {
        mapGenerator.updateSize();
        mapGenerator.GenerateMap(Vector2I.Zero, this);
    }
}