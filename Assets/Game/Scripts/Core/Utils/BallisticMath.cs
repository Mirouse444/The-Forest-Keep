using UnityEngine;

public static class BallisticMath
{
    public static bool CalculateLowArc(Vector2 start, Vector2 target, float speed, float gravity, out Vector2 direction)
    {
        direction = Vector2.zero;
        Vector2 diff = target - start;
        float x = Mathf.Abs(diff.x);
        float y = diff.y;

        float s2 = speed * speed;
        float s4 = s2 * s2;
        float root = s4 - gravity * (gravity * x * x + 2 * y * s2);

        if (root < 0) return false; 

        float angle = Mathf.Atan2(s2 - Mathf.Sqrt(root), gravity * x);
        float dirX = Mathf.Cos(angle) * Mathf.Sign(diff.x);
        float dirY = Mathf.Sin(angle);

        direction = new Vector2(dirX, dirY).normalized;
        return true;
    }
}