using UnityEngine;

[RequireComponent(typeof(Projectile), typeof(Rigidbody2D))]

public class ProjectilePhysicsMovement : MonoBehaviour
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Rigidbody2D _rigidbody;

    private const float MinMovementThreshold = 0.01f;

    private void OnEnable() => _projectile.LaunchAction += OnLaunch;
    private void OnDisable() => _projectile.LaunchAction -= OnLaunch;

    private void OnLaunch(LaunchData data)
    {
        _rigidbody.linearVelocity = data.Direction * data.Speed;
    }

    private void Update()
    {
        if (_rigidbody.linearVelocity.sqrMagnitude > MinMovementThreshold)
            ChangeRotation();
    }

    private void ChangeRotation()
    {
        float angle = Mathf.Atan2(_rigidbody.linearVelocity.y, _rigidbody.linearVelocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}