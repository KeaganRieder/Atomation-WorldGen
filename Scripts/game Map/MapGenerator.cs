namespace Atomation.GameMap;

using Godot;

/// <summary>
/// handles the game's map's generation. through
/// organizing genSteps to either generate chunks or 
/// a preview
/// </summary>
public class MapGenerator
{
    /// <summary>
    /// decides if generator configures it self to generate chunks [Chunk.CHUNK_SIZE X Chunk.CHUNK_SIZE]
    /// or if generator configures it self to generate map preview [PREVIEW_SIZE X PREVIEW_SIZE]
    /// also determines wether or not true size is total map size or just given
    /// </summary>
    public bool ChunkMode
    {
        get => chunkMode;
        set
        {
            if (value)
            {
                generationArea = new Vector2I(Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE);
            }
            chunkMode = value;
        }
    }
    private bool chunkMode;

    private Vector2I generationArea;
    private MapSettings settings;

    public MapGenerator(MapSettings settings)
    {
        this.settings = settings;
        ChunkMode = false;
    }

    public void updateSize()
    {
        if (ChunkMode)
        {
            GD.PushError("chunk Generation mode enabled size can't be changed");
            return;
        }
        generationArea = settings.worldSize;
    }

    /// <summary>
    /// run the games map generate
    /// </summary>
    public void GenerateMap(Vector2 areaOffset, Node2D node2D = null)
    {
        if (node2D == null && !ChunkMode)
        {
            GD.PushError("can't generate sense no parent is set");
            return;
        }

        GenerateNoiseMaps(areaOffset, out float[,] elevationMap, out float[,] temperatureMap, out float[,] moistureMap);

        GenStepLandScape genStepTerrain = new GenStepLandScape(elevationMap, temperatureMap, moistureMap);
        genStepTerrain.Generate(out Tile[,] generatedTiles, areaOffset, generationArea);

        // finalizing generation
        if (ChunkMode)
        {
            GD.PushError("chunk Generation mode not implemented");
            return;
        }

        for (int x = 0; x < generationArea.X; x++)
        {
            for (int y = 0; y < generationArea.Y; y++)
            {
                Tile tile = generatedTiles[x,y];
                node2D.AddChild(tile);
            }
        }

    }

    /// <summary>
    /// generates noise maps which represents the elevation, temperature and
    /// moisture of given gen area at the offset
    /// </summary>
    private void GenerateNoiseMaps(Vector2 areaOffset, out float[,] elevation, out float[,] temperature, out float[,] moisture)
    {
        GradientMapGenerator GradientMapGenerator = new GradientMapGenerator();
        NoiseMapGenerator NoiseMapGenerator = new NoiseMapGenerator(settings.elevationMapConfigs);

        elevation = NoiseMapGenerator.Generate(areaOffset, generationArea);
        temperature = GradientMapGenerator.Run(areaOffset, generationArea, settings.worldSize, settings.trueCenter);
        temperature = GenerationUtil.GenerateTemperatureMap(generationArea, temperature, elevation, settings);

        NoiseMapGenerator = new NoiseMapGenerator(settings.rainfallMapConfigs);

        moisture = GenerationUtil.GenerateMoisture(generationArea, NoiseMapGenerator.Generate(areaOffset, generationArea), settings);
    }
}