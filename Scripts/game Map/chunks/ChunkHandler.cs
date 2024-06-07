namespace Atomation.GameMap;

using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// class defines and manages interaction with chunks 
/// </summary>
public class ChunkHandler
{
    private List<Vector2I> lastLoadedChunks;
    private Dictionary<Vector2, Chunk> chunks;
    private Node2D map;

    public ChunkHandler(Node2D map)//maybe make this contain a generator
    {
        this.map = map;
        chunks = new Dictionary<Vector2, Chunk>();
        lastLoadedChunks = new List<Vector2I>();
    }

    /// <summary> adds chunk at given position </summary>
    public void AddChunk(Vector2 chunkCord, Chunk chunk)
    {
        if (HasChunk(chunkCord))
        {
            GD.PushError($"chunk array already contains a chunk at {chunkCord}");
            return;
        }
        chunks[chunkCord] = chunk;
        map.AddChild(chunk);

    }

    /// <summary> gets chunk at given position </summary>
    public Chunk GetChunk(Vector2 chunkCord)
    {
        if (!HasChunk(chunkCord))
        {
            return null;
        }

        return chunks[chunkCord];
    }

    /// <summary> returns the positions of the chunks as a list</summary>
    public List<Vector2I> GetChunkList()
    {
        List<Vector2I> chunkCords = new List<Vector2I>();
        foreach (var chunk in chunks)
        {
            var vector2I = new Vector2I(Mathf.FloorToInt(chunk.Key.X), Mathf.FloorToInt(chunk.Key.Y));
            chunkCords.Add(vector2I);
        }

        return chunkCords;
    }

    /// <summary>
    /// returns a list of teh position form the chunks load in the last
    /// chunk update
    /// </summary>
    public List<Vector2I> GetLastLoaded()
    {
        return lastLoadedChunks;
    }

    /// <summary> checks if chunk is at given cord  </summary>
    public bool HasChunk(Vector2 chunkCord)
    {        
        return chunks.ContainsKey(chunkCord);
    }

    /// <summary> erase chunk at given position </summary>
    public void RemoveChunk(Vector2 chunkCord)
    {
        if (!HasChunk(chunkCord))
        {
            return;
        }
        chunks[chunkCord].Clear();
        chunks.Remove(chunkCord);
    }

    /// <summary> clears all chunks from the map </summary>
    public void Clear()
    {
        foreach (var chunk in chunks)
        {
            chunk.Value.Clear();
        }
        chunks.Clear();
    }

    /// <summary> erase chunk at given position </summary>
    public void EraseChunk(Vector2 chunkCord)
    {
        if (!HasChunk(chunkCord))
        {
            return;
        }
    }

    /// <summary> generates chunk at given position </summary>
    private void GenerateChunk(Vector2 cord)
    {
        // GD.PushError("Chunk Gen Not Implemented");
        Chunk generatedChunk = new Chunk(cord);
        // Map.Instance.GetGenerator();//(cord);
        AddChunk(cord, generatedChunk);
    }

    /// <summary> loads chunk at given position </summary>
    public void LoadChunk(Vector2 cord)
    {
        GD.Print($"attempting to load {cord}");
        if (!lastLoadedChunks.Contains((Vector2I)cord))
        {
            lastLoadedChunks.Add((Vector2I)cord);
        }

        if (!HasChunk(cord))
        {
            GenerateChunk(cord);
            return;
        }
        if (chunks[cord].Loaded())
        {
            return;
        }

        chunks[cord].Load();
    }

    /// <summary> unloads chunk at given position </summary>
    public void UnloadChunk(Vector2 cord)
    {
        GD.Print($"attempting to unload {cord}");

        if (lastLoadedChunks.Contains((Vector2I)cord))
        {
            lastLoadedChunks.Remove((Vector2I)cord);
        }

        if (!HasChunk(cord))
        {
            GD.PushError($"attempt to unload non existent chunk at {cord}");
            return;
        }
        if (!chunks[cord].Loaded())
        {
            return;
        }
        GD.Print($"Unloading chunks");
        chunks[cord].Unload();
    }
}