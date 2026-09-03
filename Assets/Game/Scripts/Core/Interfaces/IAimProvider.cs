using UnityEngine;

public interface IAimProvider
{
    Vector2 GetAimDirection(Vector2 originPoint);
}