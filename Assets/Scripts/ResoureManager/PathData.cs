public class PathData
{
    public string typeName;
    public string configStoragePath;
    public string resourceStoragePath;

    public PathData(string typeName, string resourceStoragePath, string configStoragePath)
    {
        this.typeName = typeName;
        this.configStoragePath = configStoragePath;
        this.resourceStoragePath = resourceStoragePath;
    }
}