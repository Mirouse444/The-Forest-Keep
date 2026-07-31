using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour, ITarget
{
    public Vector3 Position => transform.position;
}