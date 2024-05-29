namespace Atomation.Resources;

/// <summary>
/// class which contain constants for important file paths in 
/// for the game
/// </summary>
public static class FilePaths
{ 
    public const string ASSET_FOLDER = "assets/";

    public const string TEXTURE_FOLDER = ASSET_FOLDER + "textures/";

    public const string DEFINITION_FOLDER = "data/core/defs/";
    
    public const string TERRAIN_FOLDER = DEFINITION_FOLDER + "terrain/";
  
    public const string STRUCTURE_FOLDER = DEFINITION_FOLDER + "structures/";
    public const string ITEM_FOLDER = DEFINITION_FOLDER + "items/";

    public const string BIOME_FOLDER = DEFINITION_FOLDER + "biomes/";

    public const string CONFIG_FOLDER = "data/configs/";

    public const string KEYBINDINGS_FOLDER = CONFIG_FOLDER + "key_bindings/";

    public const string SAVE_FOLDER = "game_saves/";
}