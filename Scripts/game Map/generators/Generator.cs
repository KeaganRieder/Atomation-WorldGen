namespace Atomation.GameMap;

using Godot;

/// <summary>
/// base class for all generators in the game
/// </summary>
public abstract class Generator<ValueType>
{
    protected Vector2I genSize;
    protected Vector2I totalSize;

    protected Vector2 offset;

    protected Generator()
    {
        offset = Vector2I.Zero;
        SetSize();
        SetTotalSize();
    }

    /// <summary>
    /// sets generators offset
    /// </summary>
    public virtual void SetOffset(Vector2 offset)
    {
        this.offset = offset;
    }

    /// <summary> 
    /// sets generators size 
    /// </summary>
    public virtual void SetSize(Vector2I size = default)
    {
        if (size == default)
        {
            genSize = new Vector2I(Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE);
            return;
        }
        genSize = size;
    }

    /// <summary> 
    /// sets the total size
    /// </summary>
    public virtual void SetTotalSize(Vector2I size = default)
    {
        // set to be the given gen size
        if (size == default)
        {
            totalSize = this.genSize;
            return;
        }

        size.X = (size.X == 0) ? 1 : size.X;
        size.Y = (size.Y == 0) ? 1 : size.Y;

        totalSize = size;
    }
}