using UnityEngine;

[RequireComponent(typeof(Projectile))]
public class PrijectileLinearMovement : MonoBehaviour
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Rigidbody2D _projectileRigidbody;
    [SerializeField] private LayerMask _collisionMask;
    
    private Vector2 _velocity;

    private void OnEnable() => _projectile.LaunchAction += OnLaunch;
    private void OnDisable() => _projectile.LaunchAction -= OnLaunch;

    private void OnLaunch(LaunchData data) => _velocity = data.Direction * data.Speed;

    private void FixedUpdate()
    {
        Vector2 currentPosition = transform.position;
        Vector2 movementThisFrame = _velocity * Time.fixedDeltaTime;
        float distanceThisFrame = movementThisFrame.magnitude;
        
        RaycastHit2D hit = Physics2D.Raycast(currentPosition, _velocity.normalized, distanceThisFrame, _collisionMask);

        if (hit.collider != null)
        {
            _projectileRigidbody.MovePosition(hit.point);
        }
        else
        {
            _projectileRigidbody.MovePosition(currentPosition + movementThisFrame);
        }
    }
}