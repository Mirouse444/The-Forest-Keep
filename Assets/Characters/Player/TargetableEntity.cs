using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class TargetableEntity : MonoBehaviour, ITarget
{
    public Vector3 Position => transform.position;
}
