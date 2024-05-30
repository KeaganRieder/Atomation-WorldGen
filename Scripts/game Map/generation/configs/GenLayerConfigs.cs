namespace Atomation.GameMap;

using Godot;
using Newtonsoft.Json;

/// <summary>
/// configs used to help configure how generated maps should
/// be layer
/// </summary>
public class GenLayerConfigs
{
    /// <summary>
    /// the layers generator 
    /// </summary>
    private Generator<float> layerGenerator;

    /// <summary>
    /// how much of an effect this layer has on what it's being applied to
    /// </summary>
    [JsonProperty]
    private float layerEffect;

    /// <summary>
    /// how much of an impact the layer should have each step
    /// </summary>
    private float layerHeight;

    public GenLayerConfigs() { }


    [JsonIgnore]
    public Generator<float> LayerGenerator { get => layerGenerator; set => layerGenerator = value; }
    [JsonIgnore]
    public float LayerEffect { get => layerEffect; set => layerEffect = value; }
    [JsonIgnore]
    public float LayerHeight { get => layerHeight; set => layerHeight = value; }

    public float GetLayerValue(int x, int y)
    {
        if (layerHeight == 0)
        {
            layerHeight = 1;
        }
        return (layerGenerator.GetValue(x, y)*layerGenerator.GetValue(x, y) )* layerEffect; // layerHeight) ;
    }
}