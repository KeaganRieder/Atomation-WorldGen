namespace Atomation.GameMap;

using Godot;

/// <summary>
/// generates a noise map based on settings. the map is then scale based on the 
/// map size
/// </summary>
public class NoiseGenerator : Generator<float>
{
    protected float[,] noiseMap;

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
        noiseMap = new float[genSize.X, genSize.Y];
    }

    protected override bool ValidateGenerator(){
        if (settings == null)
        {
            noiseMap = null;
            GD.PushError("can't generate settings are not provided");
            return false;
        }
        return true;
    }

    public override void RunGenerator(Vector2 offset = default, Vector2I size = default)
    {
        if (!ValidateGenerator())
        {
            return;
        }

        SetSize(size);
        SetOffset(offset);

        settings.NoiseConfigs.NoiseOffset = genOffset;

        FastNoiseLite noiseGenerator = settings.NoiseConfigs.FastNoiseLite;

        for (int x = 0; x < genSize.X; x++)
        {
            for (int y = 0; y < genSize.Y; y++)
            {
                float sampleX = x / totalSize.X;
                float sampleY = y / totalSize.Y;

                float noise = noiseGenerator.GetNoise2D(sampleX, sampleY);
                noiseMap[x, y] = Mathf.InverseLerp(-1, 1, noise); //normalize value to be between 0 and 1

                SetMax(noiseMap[x, y]);
                SetMin(noiseMap[x, y]);
            }
        }

        PrintMinMax();
    }

    /// <summary> 
    /// returns a value at the given point form noise map generated form the generator
    /// </summary>
    public override float GetValue(int x, int y)
    {
        if (x < 0 && y < 0 && x > genSize.X && y > genSize.Y)
        {
            GD.PushError($"Provided values {x}, {y} are outside generators size {genSize}");
            return 0;
        }
        float value = noiseMap[x, y];
        return value;
    }

    /// <summary> 
    /// returns the noise map generated form the generator
    /// </summary>
    public override float[,] GetMap()
    {
        return noiseMap;
    }


    public override void ClearMap()
    {
        noiseMap = null;
    }
}