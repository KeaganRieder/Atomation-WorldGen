namespace Atomation.GameMap;

using Godot;

/// <summary>
/// generates a noise map based on settings. the map is then scale based on the 
/// map size
/// TODO: make smoothing section
/// </summary>
public class NoiseGenerator : Generator<float>
{
    protected GeneratorConfigs settings;

    public NoiseGenerator() : base() { }

    public NoiseGenerator(GeneratorConfigs settings = null) : base()
    {
        SetSettings(settings);
    }

    public virtual void SetSettings(GeneratorConfigs settings)
    {
        this.settings = settings;
    }

    public override void SetSize(Vector2I size)
    {
        base.SetSize(size);
    }

    protected override bool Validate()
    {
        if (settings == null)
        {
            GD.PushError("can't generate settings are not provided");
            return false;
        }
        return true;
    }

    public override float[,] Run(Vector2 offset = default, Vector2I size = default)
    {
        if (!Validate())
        {
            return default;
        }

        SetSize(size);
        SetOffset(offset);
        float[,] noiseMap = new float[genSize.X, genSize.Y];

        settings.NoiseConfigs.NoiseOffset = genOffset;

        FastNoiseLite noiseGenerator = settings.NoiseConfigs.FastNoiseLite;
        float totalWidth = totalSize.X;
        float totalHeight = totalSize.Y;

        for (int x = 0; x < genSize.X; x++)
        {
            for (int y = 0; y < genSize.Y; y++)
            {
                float sampleX = (x / totalWidth);
                float sampleY = (y / totalHeight);
                float noise = noiseGenerator.GetNoise2D(sampleX, sampleY);

                if (settings.NoiseConfigs.Normalized)
                {
                    noiseMap[x, y] = (noise + 1) / 2; //make between 0-1
                }
                else
                {
                    noiseMap[x, y] = noise;
                }

                SetMax(noiseMap[x, y]);
                SetMin(noiseMap[x, y]);
            }
        }

        PrintMinMax();
        return noiseMap;
    }

}