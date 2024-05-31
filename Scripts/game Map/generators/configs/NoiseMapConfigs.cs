namespace Atomation.GameMap;

using Godot;

/// <summary>
/// defines configs used for configuring how a noise map gets
/// generated
/// </summary>
public class NoiseMapConfigs
{
    private FastNoiseLite fastNoiseLite;

    private int seed;
    /// <summary>
    /// zooms in or out the noise. lower values lead to more zoom out, while higher
    /// leads to more zoomed in
    /// </summary>
    private float scale;
    private float frequency;
    private float lacunarity;
    private float gain;
    private int octaves;
    private Vector2 noiseOffset;

    /// <summary>
    /// decides if value generated should be between 0 - 1 (normalized = true) 
    /// or -1 - 1 (normalized != true) 
    /// </summary>
    private bool normalize;
    
    public NoiseMapConfigs() { fastNoiseLite = new FastNoiseLite();}

    public bool Normalized { get => normalize; set => normalize = value; }
    public int Seed
    {
        get => seed;
        set
        {
            seed = value;
            fastNoiseLite.Seed = seed;
        }
    }
    public float Scale { get => scale; set => scale = value; }
    public float Frequency
    {
        get => frequency;
        set
        {
            frequency = value;
            fastNoiseLite.Frequency = value;
        }
    }

    public float Lacunarity
    {
        get => lacunarity;
        set
        {
            lacunarity = value;
            fastNoiseLite.FractalLacunarity = lacunarity;
        }
    }
    public float Gain
    {
        get => gain;
        set
        {
            gain = value;
            fastNoiseLite.FractalGain = value;
        }
    }

    public int Octaves
    {
        get => octaves;
        set
        {
            octaves = value;
            fastNoiseLite.FractalOctaves = value;
        }
    }

    public Vector2 NoiseOffset
    {
        get => noiseOffset;
        set
        {
            noiseOffset = value;
            fastNoiseLite.Offset = new Vector3(noiseOffset.X, noiseOffset.Y, 0);
        }
    }

    public FastNoiseLite.NoiseTypeEnum NoiseType
    {
        get => fastNoiseLite.NoiseType;
        set { fastNoiseLite.NoiseType = value; }
    }

    public FastNoiseLite.FractalTypeEnum FractalType
    {
        get => fastNoiseLite.FractalType;
        set { fastNoiseLite.FractalType = value; }
    }

    public FastNoiseLite FastNoiseLite { get => fastNoiseLite; private set => fastNoiseLite = value; }

}