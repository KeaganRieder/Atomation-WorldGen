namespace Atomation.GameMap;

using Godot;

/// <summary>
/// base class for noise generators
/// </summary>
public class NoiseGenerators : Generator<float>
{
    protected float[,] noiseMap;

    /// <summary> 
    /// runs the generator and returns the outcome as a 2d array
    /// </summary>
    public virtual float[,] Generate(Vector2 offset = default, Vector2I size = default)
    {
        GD.PushError("GenStep generation is not implemented");
        return default;
    }    
}