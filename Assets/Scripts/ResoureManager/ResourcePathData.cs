using System.Collections.Generic;

public class ResourcePathData
{
    public static Dictionary<string, PathData> ResourcePathDatas = new Dictionary<string, PathData>
    {
        {"GameObject", new PathData("prefab", "Assets/Resources/Prefabs", "Assets/StreamingAssets/PrefabConfig.txt")},
        {"Texture2D", new PathData("texture", "Assets/Resources/Textures", "Assets/StreamingAssets/TextureConfig.txt")},
        {"AudioClip", new PathData("audioclip", "Assets/Resources/Sounds", "Assets/StreamingAssets/SoundConfig.txt")},
        {"ScriptableObject", new PathData("scriptableobject", "Assets/Resources/ScriptableObjects", "Assets/StreamingAssets/SOConfig.txt")}
    };
}