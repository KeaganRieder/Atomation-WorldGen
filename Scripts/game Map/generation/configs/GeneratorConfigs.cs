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

    [JsonConstructor]
    public GeneratorConfigs() {}

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

    public float Multiplier { get => multiplier; set => multiplier = value; }
}