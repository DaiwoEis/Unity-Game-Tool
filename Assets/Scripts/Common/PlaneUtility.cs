using UnityEngine;

public static class PlaneUtility 
{
    public static Vector3 Direction(Vector3 direction)
    {
        direction.y = 0f;
        return direction.normalized;
    }

    public static float Distance(Vector3 pos1, Vector3 pos2)
    {
        pos1.y = pos2.y = 0f;
        return Vector3.Distance(pos1, pos2);
    }

    public static float SqrtDistance(Vector3 pos1, Vector3 pos2)
    {
        pos1 = pos1 - pos2;
        pos1.y = 0f;
        return pos1.sqrMagnitude;
    }

    public static bool IsArrive(Vector3 origin, Vector3 dest, float threshold = 0.1f)
    {
        return SqrtDistance(origin, dest) < threshold * threshold;
    }
}
