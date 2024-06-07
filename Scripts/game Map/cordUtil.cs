namespace Atomation.GameMap;

using Godot;

/// <summary>
/// defines utility used to convert between different cord types
/// </summary>
public static class cordUtil
{
    /// <summary>
    /// converts world position to tile position which is equal to
    /// Mathf.FloorToInt(worldPosition / map.CELL_SIZE0
    /// </summary>
    public static Vector2I WorldToTile(this Vector2 worldPosition)
    {
        Vector2I tilePosition = new Vector2I(
            Mathf.FloorToInt(worldPosition.X / Map.CELL_SIZE),
            Mathf.FloorToInt(worldPosition.Y / Map.CELL_SIZE)
        );
        return tilePosition;
    }

    /// <summary>
    /// converts tilePosition to worldPosition which is equal to
    /// Mathf.FloorToInt(tilePosition * map.CELL_SIZE)
    /// </summary>
    public static Vector2I TileToWorld(this Vector2 tilePosition)
    {
        Vector2I worldPosition = new Vector2I(
            Mathf.FloorToInt(tilePosition.X * Map.CELL_SIZE),
            Mathf.FloorToInt(tilePosition.Y * Map.CELL_SIZE)
        );
        return worldPosition;
    }

    /// <summary>
    /// converts worldPosition to chunkPosition which is equal to
    /// Mathf.FloorToInt(worldPosition / map.CELL_SIZE / Chunk.CHUNK_SIZE)
    /// </summary>
    public static Vector2I WorldToChunk(this Vector2 worldPosition)
    {
        Vector2I chunkPosition = new Vector2I(
            Mathf.FloorToInt(worldPosition.X / Map.CELL_SIZE / Chunk.CHUNK_SIZE),
            Mathf.FloorToInt(worldPosition.Y / Map.CELL_SIZE / Chunk.CHUNK_SIZE)
        );
        // GD.Print($"{worldPosition} {chunkPosition}");
        return chunkPosition;
    }

    /// <summary>
    /// converts chunkPosition to worldPosition which is equal to
    /// Mathf.FloorToInt(worldPosition * map.CELL_SIZE * Chunk.CHUNK_SIZE)
    /// </summary>
    public static Vector2I ChunkToWorld(this Vector2 chunkPosition)
    {
        Vector2I worldPosition = new Vector2I(
            Mathf.FloorToInt(chunkPosition.X / Map.CELL_SIZE / Chunk.CHUNK_SIZE),
            Mathf.FloorToInt(chunkPosition.Y / Map.CELL_SIZE / Chunk.CHUNK_SIZE)
        );
        return worldPosition;
    }
}