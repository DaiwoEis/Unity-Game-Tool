public static class LayerUtility
{
    public static bool InLayerMask(int layer, int layerMask)
    {
        return (layer & layerMask) == layer;
    }
}