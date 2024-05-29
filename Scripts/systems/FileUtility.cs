namespace Atomation.Resources;

using Godot;
using System;
using System.IO;
using Newtonsoft.Json;

/// <summary>
/// utility class defining function which handle reading in file 
/// or writing object to files
/// </summary>
public static class FileUtility
{
    /// <summary>
    /// writes the given object to a json file. naming that to the given name 
    /// and writing it to given path. using given JsonSerializerSettings
    /// </summary>
    public static void ToJson<ObjType>(this ObjType obj, string filePath, string fileName, JsonSerializerSettings settings = null)
    {
        if (settings == null)
        {
            settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
            };
        }

		filePath = ProjectSettings.GlobalizePath(filePath);
        if (!Directory.Exists(filePath))
		{
			Directory.CreateDirectory(filePath);
		}

		filePath += fileName + ".json";
		string jsonData = JsonConvert.SerializeObject(obj, settings);
        try
		{
			File.WriteAllText(filePath, jsonData);
		}
		catch (Exception error)
		{
			GD.PushError("Error writing file: " + error.Message);
		}
    }

    /// <summary>
	/// reads json file from location and formats it to correct object'
	/// which it then returns
	/// </summary>
	public static ObjType ReadJsonFile<ObjType>(string filePath, string fileName)
	{
		filePath += fileName + ".json";
		return ReadJsonFile<ObjType>(filePath);
	}

    /// <summary>
	/// reads json file from location and formats it to correct object'
	/// which it then returns
	/// </summary>
    public static ObjType ReadJsonFile<ObjType>(string filePath)
	{
		if (File.Exists(filePath))
		{
			string jsonData = File.ReadAllText(filePath);
			ObjType convertedData = JsonConvert.DeserializeObject<ObjType>(jsonData);
			return convertedData;
		}
		else
		{
			GD.PushError($"File: {filePath} doesn't exist");
			return default;
		}
	}

    /// <summary>
	/// reads in .png files, and converts it to a texture2d object at the given size. if 
	/// the path doesn't contain a image then set texture to default Undefined one
	/// </summary>
	public static Texture2D ReadTexture(string filePath, Vector2I TextureSize)
	{
		Image image;

		if (File.Exists(filePath))
		{
			image = Image.LoadFromFile(filePath);
		}
		else
		{
			filePath = FilePaths.TEXTURE_FOLDER + "DefaultTexture.png";
			image = Image.LoadFromFile(filePath);
		}


		if (image.GetSize() != TextureSize)
		{
			image.Resize(TextureSize.X, TextureSize.Y, Image.Interpolation.Bilinear);

		}

		return ImageTexture.CreateFromImage(image);
		;
	}

	/// <summary>
	/// reads in a collection of .png files, converting them to be a texture2d which is used as objects graphics
	/// </summary>
	public static Texture2D[] ReadTextureGroup(string filePath, Vector2I TextureSize, int variants)
	{

		Texture2D[] textureArray = new Texture2D[variants];

		for (int i = 0; i < variants; i++)
		{
			string path = filePath + "_" + i;

			textureArray[i] = ReadTexture(path, TextureSize);
		}

		return default;
	}
}
