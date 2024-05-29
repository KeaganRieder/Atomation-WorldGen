namespace Atomation.GameMap;

using System;
using Godot;

public partial class Chunk : Node2D
{
    public const int CHUNK_SIZE = 16;

    Vector2 chunkPosition;

    private bool loaded;

    private Grid chunkGrid;
    // private Rect2I chunkArea;

    public Chunk()
    {
    }

    public Chunk(Vector2 position)
    {
        SetPosition(position);

        // chunkArea = new Rect2I();

        chunkGrid = new Grid(false);

        // Unload();
    }
    
    /// <summary>
    /// clears chunk
    /// </summary>
    public void Clear()
    {
        chunkGrid.Clear();
    }

    /// <summary> 
    /// sets chunks position to given 
    /// </summary>
    public void SetPosition(Vector2 cord)
    {
        chunkPosition = cord;
        Position = cord;
    }

    /// <summary> 
    /// sets terrain at given position
    /// </summary>
    public void SetTerrain(Vector2 cord, Sprite2D terrain)
    {
        chunkGrid.SetValue(cord, terrain, 0);
    }

    /// <summary> 
    /// gets terrain at given position 
    /// </summary>
    public Sprite2D GetTerrain(Vector2 cord)
    {
        Sprite2D terrain = null;

        object obj = chunkGrid.GetValue(cord, 0);
        if (obj is Sprite2D)
        {
            terrain = obj as Sprite2D;
        }
        else
        {
            throw new InvalidOperationException("Object is not terrain");
        }

        return terrain;
    }

    /// <summary> 
    /// unloads chunk 
    /// </summary>
    public void Unload()
    {
        loaded = false;
        Visible = false;
    }

    /// <summary>
    /// loads chunk
    /// </summary>
    public void Load()
    {
        loaded = true;
        Visible = true;
    }

    /// <summary>
    /// returns if chunks loaded (true) or unloaded (false)
    /// </summary>
    public bool Loaded()
    {
        return loaded;
    }
}