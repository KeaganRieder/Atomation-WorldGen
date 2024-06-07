namespace Atomation.GameMap;

using Godot;

public class GradientMapGenerator : NoiseGenerators
{
    private float center;

    public GradientMapGenerator()
    {
    }

    public override void SetSize(Vector2I size = default)
    {
        base.SetSize(size);
        noiseMap = new float[size.X, size.Y];
    }

    public float[,] Run(Vector2 offset = default, Vector2I size = default, Vector2I totalSize = default, bool trueCenter = false)
    {
        SetSize(size);
        SetTotalSize(totalSize);
        SetOffset(offset);
        float[,] noiseMap = new float[size.X, size.Y];

        center = trueCenter ? this.totalSize.Y / 2 : 1;

        for (int x = 0; x < size.X; x++)
        {
            for (int y = 0; y < size.Y; y++)
            {
                float val = DistanceFromCenter(y) / totalSize.Y;
                noiseMap[x, y] = val;
            }
        }

        return noiseMap;
    }

    /// <summary>
    /// calculates the given cell is from the equator
    /// </summary>
    private float DistanceFromCenter(int y)
    {
        float distance = Mathf.Abs(y + offset.Y - center);

        return distance;
    }
}