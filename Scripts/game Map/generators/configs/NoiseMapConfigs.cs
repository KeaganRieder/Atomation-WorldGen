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
    private float scale;
    private float frequency;
    private float lacunarity;
    private float gain;
    private int octaves;
    private Vector2 noiseOffset;
    private bool normalize;

    public NoiseMapConfigs() { fastNoiseLite = new FastNoiseLite(); }

    /// <summary>
    /// decides the range of values generated
    /// true = 0 - 1
    /// false -1 to 1
    /// </summary>
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
    /// <summary>
    /// zooms in or out the noise. lower values lead to more zoom out, while higher
    /// leads to more zoomed in
    /// </summary>
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
    /// <summary>
    /// Frequency multiplier between subsequent octaves. 
    /// Increasing this value results in higher octaves producing noise with finer details and a rougher appearance. 
    /// </summary>
    public float Lacunarity
    {
        get => lacunarity;
        set
        {
            lacunarity = value;
            fastNoiseLite.FractalLacunarity = lacunarity;
        }
    }

    /// <summary>
    /// Determines the strength of each subsequent layer of noise in fractal noise.
    /// A low value places more emphasis on the lower frequency base layers,
    /// while a high value puts more emphasis on the higher frequency layers.
    /// </summary>
    public float Gain
    {
        get => gain;
        set
        {
            gain = value;
            fastNoiseLite.FractalGain = value;
        }
    }

    /// <summary>
    /// how many noise layers there are
    /// </summary>
    public int Octaves
    {
        get => octaves;
        set
        {
            octaves = value;
            fastNoiseLite.FractalOctaves = value;
        }
    }

    /// <summary>
    /// offset of where generated noise is pulled from
    /// </summary>
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