using UnityEngine;

[RequireComponent(typeof(Projectile))]
public class LinearMovement : MonoBehaviour
{
    [SerializeField] private Projectile _projectile;

    private Vector2 _velocity;
    private bool _isMoving;

    private void OnEnable()
    { 
        _projectile.LaunchAction += OnLaunch; 
    }

    private void OnDisable()
    {
        _projectile.LaunchAction -= OnLaunch;
        _isMoving = false;
    }

    private void OnLaunch(LaunchData data)
    {
        _velocity = data.Direction * data.Speed;
        _isMoving = true;
    }

    private void Update()
    {
        if (_isMoving)
            transform.Translate(_velocity * Time.deltaTime, Space.World);
    }
}