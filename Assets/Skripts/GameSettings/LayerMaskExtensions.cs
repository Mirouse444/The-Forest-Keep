using UnityEngine;

internal static class LayerMaskExtensions
{
    public static bool Contains(this LayerMask mask, int layerIndex)
    {
        return ((1 << layerIndex) & mask.value) != 0;
    }
}