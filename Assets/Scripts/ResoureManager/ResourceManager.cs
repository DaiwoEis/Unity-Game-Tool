using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class ResourceManager
{
    private static Dictionary<string, Dictionary<string, string>> map;

    static ResourceManager()
    {        
        map = new Dictionary<string, Dictionary<string, string>>();
        foreach (var resoureData in ResourcePathData.ResourcePathDatas)
            BuildConfig(resoureData.Key, resoureData.Value);
    }

    private static void BuildConfig(string typeName, PathData pathData)
    {
        if (!map.ContainsKey(typeName))
            map[typeName] = new Dictionary<string, string>();

        string mapText = GetConfigFile(pathData.configStoragePath);

        using (StringReader reader = new StringReader(mapText))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var keyValue = line.Split('=');
                map[typeName].Add(keyValue[0], keyValue[1]);
            }
        }
    }

    public static string GetConfigFile(string path)
    {
        if (Application.platform != RuntimePlatform.Android)
            path = "file://" + Path.GetFullPath(path);
        WWW www = new WWW(path);
        while (true)
        {
            if (www.isDone)
                return www.text;
        }
    }

    public static T Load<T>(string resourceName) where T : Object
    {
        string typeName = PathHelper.RemoveNameSpace(typeof(T).ToString());
        if (!map.ContainsKey(typeName))
        {
            Debug.Log(string.Format("Type {0} donest has path data.", typeName));
            return null;
        }

        if (!map[typeName].ContainsKey(resourceName))
        {
            Debug.Log(string.Format("Resource {0} donest has path data.", resourceName));
            return null;
        }

        string path = map[typeName][resourceName];
        return Resources.Load<T>(path);
    }

}
