using System;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private LayerMask _groundLayer;

    public event Action<Collider2D> OnTargetHit;
    public event Action<Collider2D> OnGroundHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_enemyLayer.Contains(collision.gameObject.layer))
        {
            OnTargetHit?.Invoke(collision);
        }
        else if(_groundLayer.Contains(collision.gameObject.layer))
        {
            OnGroundHit?.Invoke(collision);
        }
    }
}