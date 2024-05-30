namespace Atomation.GameMap;

using Godot;

/// <summary>
/// defines configs used for configuring how a noise map gets
/// generated
/// </summary>
public class NoiseConfigs
{
    protected FastNoiseLite fastNoiseLite;

    protected int seed;
    protected float frequency;
    protected float lacunarity;
    protected float gain;
    protected int octaves;
    protected Vector2 noiseOffset;

    private bool normalize;

    public NoiseConfigs() { fastNoiseLite = new FastNoiseLite(); }

    public int Seed
    {
        get => seed;
        set
        {
            seed = value;
            fastNoiseLite.Seed = seed;
        }
    }

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

    public bool Normalized { get => normalize; set => normalize = value; }
}