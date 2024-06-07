
using Atomation.GameMap;
using Atomation.Resources;
using Godot;

public partial class Tile : Node2D
{
    public float Elevation;
    public float Moisture;
    public float Temperature;

    private int gridLayer = 1;

    private Sprite2D graphic;

    Vector2I tileCord;

    public Tile(Vector2 cord)
    {
        Name = $"{cord}";
        graphic = new Sprite2D();
        SetPosition(cord);
        SetTexture();
        AddChild(graphic);
    }

    public void SetPosition(Vector2 cord)
    {
        tileCord = (Vector2I)cord;

        Position = tileCord;
    }

    public void SetTexture()
    {
        Texture2D texture;

        texture = FileUtility.ReadTexture("Undefined", new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE));

        graphic.Texture = texture;
    }

    public void SetColor(Color color)
    {
        graphic.Modulate = color;
    }

    /// <summary>
    /// returns the layer the given object should be on in a grid
    /// </summary>
    public int GetLayer()
    {
        return gridLayer;
    }

}