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

    public ChunkHandler()
    {
        chunks = new Dictionary<Vector2, Chunk>();
        lastLoadedChunks = new List<Vector2I>();
    }

    /// <summary> adds chunk at given position </summary>
    public void AddChunk(Vector2 cord, Chunk chunk)
    {
        cord = cord.WorldToChunk();

        if (!HasChunk(cord))
        {
            GD.PushError($"chunk array already contains a chunk at {cord}");
        }
        chunks[cord] = chunk;
    }

    /// <summary> gets chunk at given position </summary>
    public Chunk GetChunk(Vector2 cord)
    {
        cord = cord.WorldToChunk();

        if (!HasChunk(cord))
        {
            return null;
        }

        return chunks[cord];
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
    public bool HasChunk(Vector2 cord)
    {
        cord = cord.WorldToChunk();

        return chunks.ContainsKey(cord);
    }

    /// <summary> erase chunk at given position </summary>
    public void RemoveChunk(Vector2 cord)
    {
        cord = cord.WorldToChunk();

        if (!HasChunk(cord))
        {
            return;
        }
        chunks[cord].Clear();
        chunks.Remove(cord);
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
    public void EraseChunk(Vector2 cord)
    {
        cord = cord.WorldToChunk();

        if (!HasChunk(cord))
        {
            return;
        }
    }

    /// <summary> generates chunk at given position </summary>
    private void GenerateChunk(Vector2 cord)
    {
        cord = cord.WorldToChunk();
        Chunk generatedChunk = new Chunk(cord);
        Map.Instance.GenerateChunk(cord);
        AddChunk(cord, generatedChunk);
    }

    /// <summary> loads chunk at given position </summary>
    public void LoadChunk(Vector2 cord)
    {
        cord = cord.WorldToChunk();

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
        cord = cord.WorldToChunk();

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
        chunks[cord].Unload();
    }
}