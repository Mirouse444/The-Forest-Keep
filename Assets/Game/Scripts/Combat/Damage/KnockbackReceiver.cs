using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KnockbackReceiver : MonoBehaviour, IKnockbackable
{
    [SerializeField] private Vector2 _knockbackTrajectory = new Vector2(1f, 0.5f);
    [SerializeField] private float _stunDuration = 0.25f; 

    private Rigidbody2D _rigidbody;
    
    public event Action<float> KnockbackApplied;

    private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

    public void ApplyKnockback(Vector2 force)
    {
        if (force == Vector2.zero) return;

        _rigidbody.linearVelocity = Vector2.zero;

        float incomingPower = force.magnitude;
        float directionX = force.x == 0 ? 1f : Mathf.Sign(force.x);

        Vector2 normalizedTrajectory = _knockbackTrajectory.normalized;

        Vector2 finalKnockback = new Vector2(
            normalizedTrajectory.x * directionX * incomingPower,
            Mathf.Abs(normalizedTrajectory.y) * incomingPower
        );

        _rigidbody.AddForce(finalKnockback, ForceMode2D.Impulse);

        KnockbackApplied?.Invoke(_stunDuration);
    }
}