using System.IO;
using UnityEditor;

public class GenerateResourcePathConfig
{
    [MenuItem("Tools/GenerateResourcePathConfigs")]
    public static void GeneratePathConfigs()
    {
        foreach (var pathData in ResourcePathData.ResourcePathDatas.Values)
            GeneratePathConfig(pathData);
    }

    public static void GeneratePathConfig(PathData pathData)
    {
        string[] guids =
            AssetDatabase.FindAssets("t:" + pathData.typeName, new[] {pathData.resourceStoragePath});
        for (var i = 0; i < guids.Length; i++)
        {
            string completePath = AssetDatabase.GUIDToAssetPath(guids[i]);
            var prefabName = Path.GetFileNameWithoutExtension(completePath);
            string path = completePath.Replace(Path.GetExtension(completePath), "").Replace("Assets/Resources/", "");
            guids[i] = prefabName + "=" + path;
        }
        File.WriteAllLines(pathData.configStoragePath, guids);
        AssetDatabase.Refresh();
    }
}