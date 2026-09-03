using UnityEngine;

public class TargetableEntity : MonoBehaviour, ITarget
{
    public Vector3 Position => transform.position;
}
