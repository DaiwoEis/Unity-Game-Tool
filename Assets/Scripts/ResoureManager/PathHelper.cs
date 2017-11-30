using System.Linq;

public static class PathHelper 
{

    public static string RemoveNameSpace(string typeName)
    {
        var splitsStr = typeName.Split('.').ToList();
        return splitsStr[splitsStr.Count - 1];
    }
}
