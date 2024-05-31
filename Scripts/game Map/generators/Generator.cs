namespace Atomation.GameMap;

using Godot;

/// <summary>
/// base class for all things related to generation
/// </summary>
public abstract class Generator<ValueType>
{

    protected Vector2I size;
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
            this.size = new Vector2I(Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE);
            return;
        }
        this.size = size;
    }

    /// <summary> 
    /// sets the total size
    /// </summary>
    public virtual void SetTotalSize(Vector2I size = default)
    {
        if (size == default)
        {
            totalSize = Vector2I.One;
            return;
        }

        size.X = (size.X == 0) ? 1 : size.X;
        size.Y = (size.Y == 0) ? 1 : size.Y;

        totalSize = size;
    }

    /// <summary> 
    /// runs the generator and returns the outcome
    /// </summary>
    public virtual ValueType[,] Run(Vector2 offset = default, Vector2I size = default)
    {
        GD.PushError("Generate is not implemented");
        return default;
    }


}