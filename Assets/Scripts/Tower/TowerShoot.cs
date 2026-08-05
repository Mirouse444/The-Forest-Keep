using UnityEngine;

[RequireComponent(typeof(TargetScanner))]

public class TowerShoot : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float _attackCooldown = 2f;
    [SerializeField] private Transform _shootPlace;
    [SerializeField] private ProjectileLauncher _launcher;

    [Header("Physics (Must match arrow Rigidbody2D)")]
    [SerializeField] private float _gravityScale = 1f;

    private TargetScanner _scanner;
    private float _lastAttackTime;

    private void Awake()
    {
        _scanner = GetComponent<TargetScanner>();
    }


}