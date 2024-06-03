namespace Atomation.GameMap;

using Godot;

public class NoiseMapGenerator : Generator<float>
{
    private float[,] noiseMap;
    private NoiseMapConfigs configs;

    public NoiseMapGenerator(NoiseMapConfigs settings = null)
    {
        Configure(settings);
    }

    public void Configure(NoiseMapConfigs settings)
    {
        this.configs = settings;
    }

    public override float[,] Run(Vector2 offset = default, Vector2I size = default)
    {
        if (configs == null)
        {
            GD.PushError("Can't Generate settings aren't set");
            return default;
        }
        SetSize(size);
        SetOffset(offset);
        float[,] noiseMap = new float[size.X, size.Y];

        configs.NoiseOffset = offset;

        FastNoiseLite noiseGenerator = configs.FastNoiseLite;

        for (int x = 0; x < size.X; x++)
        {
            for (int y = 0; y < size.Y; y++)
            {
                float sampleX = x / configs.Scale;
                float sampleY = y / configs.Scale;
                float noise = noiseGenerator.GetNoise2D(sampleX, sampleY);

                if (configs.Normalized)
                {
                    noiseMap[x, y] = Mathf.InverseLerp(-1,1,noise);//make between 0-1
                }
                else
                {
                    noiseMap[x, y] = noise;
                }
            }
        }
        return noiseMap;
    }


}