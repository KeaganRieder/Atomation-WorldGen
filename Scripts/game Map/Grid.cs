namespace Atomation.GameMap;

using System.Collections.Generic;
using Godot;

/// <summary>
/// defines Atomation's world's grid, which is used to place and story
/// things like terrain and buildings on
/// </summary>
public class Grid
{
    //holds layers of dictionary that contain values for each position
    private Dictionary<int, Dictionary<Vector2, object>> grid;

    public Grid(bool Loading = false)
    {
        if (!Loading)
        {
            grid = new Dictionary<int, Dictionary<Vector2, object>>();
        }
    }

    /// <summary>
    /// set value at given position
    /// </summary>
    public void SetValue(Vector2 cord, object value, int layer = -1)
    {
        if (layer < 0)
        {
            layer = 0;
        }
        if (!HasLayer(layer))
        {
            AddLayer(layer);
        }

        grid[layer][cord] = value;
    }

    /// <summary>
    /// gets value at given position
    /// </summary>
    public object GetValue(Vector2 cord, int layer)
    {
        if (HasLayer(layer))
        {
            return grid[layer][cord];
        }
        return default;
    }
    
    /// <summary>
    /// returns grids cells as a list
    /// </summary>
    public List<Vector2> GetCells(int layer)
    {
        return new List<Vector2>(grid[layer].Keys);
    }

    /// <summary>
    /// adds layers
    /// </summary>
    private void AddLayer(int layer)
    {
        if (!grid.ContainsKey(layer))
        {
            grid[layer] = new Dictionary<Vector2, object>();
        }
    }

    /// <summary>
    /// clears grid removing all cells
    /// </summary>
    public void Clear()
    {
        grid.Clear();
    }
    public void Erase(Vector2 cord, int layer)
    {
        if (HasCell(cord, layer))
        {
            grid[layer].Remove(cord);
        }
    }

    /// <summary>
    /// erase al invalid entries present in grid
    /// </summary>
    public void EraseInvalid()
    {
        for (int layer = 0; layer < GetLayerCount(); layer++)
        {
            foreach (var cell in GetCells(layer))
            {
                if (GetValue(cell, layer) == null)
                {
                    Erase(cell, layer);
                }
            }
        }
    }

    /// <summary>
    /// returns of cell is present at given cord
    /// </summary>
    public bool HasCell(Vector2 cord, int layer)
    {
        return grid[layer].ContainsKey(cord);
    }
    /// <summary>
    /// checks if grid contains layer
    /// </summary>
    public bool HasLayer(int layer)
    {
        return grid.ContainsKey(layer);
    }

    public int GetLayerCount()
    {
        return grid.Count;
    }

}