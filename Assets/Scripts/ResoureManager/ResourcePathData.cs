using System.Collections.Generic;

public class ResourcePathData
{
    public static Dictionary<string, PathData> ResourcePathDatas = new Dictionary<string, PathData>
    {
        {"GameObject", new PathData("prefab", "Assets/Resources/Prefabs", "PrefabConfig.txt")},
        {"Texture2D", new PathData("texture", "Assets/Resources/Textures", "TextureConfig.txt")},
        {"AudioClip", new PathData("audioclip", "Assets/Resources/Sounds", "SoundConfig.txt")},
        {"ScriptableObject", new PathData("scriptableobject", "Assets/Resources/ScriptableObjects", "SOConfig.txt")}
    };
}