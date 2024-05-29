namespace Atomation.GameMap;

using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// a collection of settings and configs used to configure/customize
/// how generators work
/// </summary>
public class GeneratorConfigs
{
    /// <summary>
    /// determine if a noise map gets layed or not
    /// </summary>
    [JsonProperty]
    protected bool layered = false;
    /// <summary>
    /// determines if the noise map uses true center of games map, 
    /// or not - only really used in temperature gen
    /// </summary>
    [JsonProperty]
    protected bool trueCenter = false;

    /// <summary>
    /// the base of teh generator which all values are offset
    /// to be from.
    /// </summary>
    [JsonProperty]
    protected float baseValue;

    /// <summary>
    /// a multiplier that gets applied to the value generated
    /// </summary>
    protected float multiplier;

    /// <summary>
    /// configs used to configure how a generator has it's 
    /// noise map generator if required
    /// </summary>
    [JsonProperty]
    protected NoiseConfigs noiseConfigs;

    /// <summary>
    /// collection of configs which are use to determine how 
    /// layers are added together
    ///     key = string which is usually the type of generator the 
    ///     layer's from
    ///     value = actual configs ofr generated layer 
    /// </summary>
    [JsonProperty]
    protected Dictionary<string, GenLayerConfigs> layerConfigs;

    [JsonConstructor]
    public GeneratorConfigs() { }

    [JsonIgnore]
    public bool Layered
    {
        get => layered;
        set => layered = value;
    }
    [JsonIgnore]
    public bool TrueCenter
    {
        get => trueCenter;
        set => trueCenter = value;
    }

    [JsonIgnore]
    public virtual float BaseValue
    {
        get => baseValue;
        set => baseValue = value;
    }

    [JsonIgnore]
    public virtual NoiseConfigs NoiseConfigs
    {
        get => noiseConfigs;
        set => noiseConfigs = value;
    }

    public Dictionary<string, GenLayerConfigs> LayerConfigs
    {
        get
        {
            if (!layered)
            {
                return null;
            }
            return layerConfigs;
        }
        set
        {
            if (!layered)
            {
                layerConfigs = null;
            }
            layerConfigs = value;
        }
    }

    public float Multiplier { get => multiplier; set => multiplier = value; }

    /// <summary>
    /// sets the value of teh layer for teh given config
    /// </summary>
    public void SetLayerMap(string layerKey, Generator<float> generator)
    {
        if (!layered)
        {
            GD.Print("Not Layerd");
            return;
        }

        if (layerConfigs.ContainsKey(layerKey))
        {
            // GD.Print($"{layerKey}");

            layerConfigs[layerKey].LayerGenerator =generator;
        }
        else
        {
            GD.PushError($"Attempted to add value for layer {layerKey} but none exist");
        }
    }
}