using System.IO;
using System.Text;
using UnityEditor;

public class GenerateResourcePathConfig
{
    [MenuItem("Tools/GenerateResourcePathConfigs")]
    public static void GeneratePathConfigs()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var rpd in ResourcePathData.ResourcePathDatas)
        {
            var mainKey = rpd.Key;

            sb.AppendLine("[" + mainKey + "]");

            var pathData = rpd.Value;
            string[] guids =
                AssetDatabase.FindAssets("t:" + pathData.typeName, new[] {pathData.resourceStoragePath});

            for (var i = 0; i < guids.Length; i++)
            {
                string completePath = AssetDatabase.GUIDToAssetPath(guids[i]);
                var prefabName = Path.GetFileNameWithoutExtension(completePath);
                string path = completePath.Replace(Path.GetExtension(completePath), "").Replace("Assets/Resources/", "");
                sb.AppendLine(prefabName + ":" + path);
            }

            sb.AppendLine();
        }

        File.WriteAllText("Assets/StreamingAssets/ResourcePathConfig.txt", sb.ToString());
        AssetDatabase.Refresh();
    }
}