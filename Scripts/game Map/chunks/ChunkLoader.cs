namespace Atomation.GameMap;

using System.Collections.Generic;
using Godot;

public class ChunkLoader
{
    private Node2D actor;

    private ChunkHandler chunkHandler;
    private int renderDistance;
    private int updateDelay;

    private int lastUpdate;
    private Vector2I lastPosition;

    public ChunkLoader(ChunkHandler chunkHandler)
    {
        this.chunkHandler = chunkHandler;
        renderDistance = 2;
        updateDelay = 0;

        lastUpdate = 0;
        lastPosition = new Vector2I(-1, -1);
    }

    /// <summary>
    /// checks if actor is at the same chunk they were last time, 
    /// if they are then doesn't update chunks. otherwise updates chunks
    /// </summary>
    public void TryLoading()
    {
        // int currentTime = (int)Time.GetTicksMsec();
        // if (!(currentTime - lastUpdate > updateDelay))
        // {
        //     return;
        // }
        // else
        // {
        //     lastUpdate = currentTime;
        // }

        Vector2I actorPosition = GetActorPosition(); //do player get position at some point

        if (actorPosition == lastPosition)
        {
            return;
        }
        lastPosition = actorPosition;
        UpdateLoaded(actorPosition);
    }

    /// <summary>
    /// decides base on position wether to load or unload a chunk
    /// </summary>
    private void UpdateLoaded(Vector2I position)
    {
        if (chunkHandler == null)
        {
            GD.PushError("can't load chunks chunkHandler property is null");
            return;
        }

        List<Vector2I> chunksToLoad = GetLoadedChunks(position);

        UnloadChunks(chunksToLoad);
        LoadChunks(chunksToLoad);
    }

    /// <summary>
    /// gets the actor/players position in terms of chunk cords
    /// </summary>
    private Vector2I GetActorPosition()
    {
        Vector2 actorPosition = Vector2I.Zero;
        // todo get player instance and then set that as chunk cords
        if (actor != null)
        {
            actorPosition = actor.GlobalPosition.Floor();
        }

        Vector2I chunkPosition = actorPosition.WorldToChunk();

        return chunkPosition;
    }

    /// <summary>
    /// gets required chunks to update based on given position
    /// </summary>
    private List<Vector2I> GetLoadedChunks(Vector2I actorCords)
    {
        Vector2I actorPosition = actorCords;
        List<Vector2I> chunksToLoad = new List<Vector2I>();
        for (int x = -renderDistance+1; x < renderDistance; x++)
        {
            for (int y = -renderDistance+1; y < renderDistance; y++)
            {
                Vector2I chunkCord = new Vector2I(actorPosition.X + x, actorPosition.Y + y);

                chunksToLoad.Add(chunkCord);
            }
        }

        return chunksToLoad;
    }

    /// <summary>
    /// runs through chunks at given positions and unloads all that 
    /// should be 
    /// </summary>
    private void UnloadChunks(List<Vector2I> chunksToLoad)
    {
        List<Vector2I> lastLoadedChunks = chunkHandler.GetLastLoaded();
        foreach (var chunk in lastLoadedChunks)
        {
            if (!chunksToLoad.Contains(chunk))
            {
                chunkHandler.UnloadChunk(chunk);
            }
        }
    }

    /// <summary>
    /// runs through chunks at given positions and unloads all that 
    /// should be 
    /// </summary>
    private void LoadChunks(List<Vector2I> chunksToLoad)
    {
        foreach (var toLoad in chunksToLoad)
        {
            chunkHandler.LoadChunk(toLoad);
        }
    }
}