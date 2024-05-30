namespace Atomation.GameMap;

using Godot;
using Newtonsoft.Json;

/// <summary>
/// configs for generators layers
/// </summary>
public class LayerConfig
{
    /// <summary>
    /// the layers generator 
    /// </summary>
    private float[,] layerValue;

    /// <summary>
    /// how much of an effect this layer has on what it's being applied to
    /// </summary>
    [JsonProperty]
    private float layerEffect;

    /// <summary>
    /// determines if values of the layer is negative or positive
    /// </summary>
    private bool positive = true;
    
    /// <summary>
    /// determines if values of the layer is negative or positive
    /// </summary>
    private bool squared = false;

    public LayerConfig() { }

    [JsonIgnore]
    public float[,] ValueMap { get => layerValue; set => layerValue = value; }

    [JsonIgnore]
    public float LayerEffect { get => layerEffect; set => layerEffect = value; }
    public bool Positive { get => positive; set => positive = value; }
    public bool Squared { get => squared; set => squared = value; }

    public float GetValue(int x, int y)
    {
        float val = 1;

        if (!positive)
        {
            val = -1;
        }

        if (squared)
        {
            return layerValue[x,y] * layerValue[x,y] * layerEffect * val;
        }
        return layerValue[x,y] * layerEffect * val;
    }
}