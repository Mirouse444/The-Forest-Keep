using System;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollision : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayers;

    public event Action<Collider2D, MeleeStrikeData> OnTargetHit;

    private MeleeStrikeData _currentStrikeData;
    private readonly HashSet<Collider2D> _hitEnemies = new HashSet<Collider2D>();

    public void Setup(MeleeStrikeData data)
    {
        _currentStrikeData = data;
    }

    private void OnEnable() => _hitEnemies.Clear();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_targetLayers.Contains(collision.gameObject.layer)) return;
        if (!_hitEnemies.Add(collision)) return;

        OnTargetHit?.Invoke(collision, _currentStrikeData);
    }
}