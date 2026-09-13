using Random = UnityEngine.Random;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class DamageOnPlace : MonoBehaviour
{
    [SerializeField] private int _minDamage = 10;
    [SerializeField] private int _maxDamage = 15;
    [SerializeField] private float _knockbackForce = 5f;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private LayerMask _targetLayer;
    
    private struct TargetInfo
    {
        public Collider2D Collider;
        public IDamageable Damageable;
        public IKnockbackable Knockbackable;
    }

    private readonly List<TargetInfo> _targets = new();
    
    private CancellationTokenSource _cts;
    private bool _isAttacking;
    
    public event Action OnTakeDamage;

    private void OnEnable() => _cts = new CancellationTokenSource();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_targetLayer.Contains(collision.gameObject.layer)) return;

        for (int i = 0; i < _targets.Count; i++)
            if (_targets[i].Collider == collision) return;

        collision.TryGetComponent(out IDamageable damageable);
        collision.TryGetComponent(out IKnockbackable knockbackable);

        if (damageable == null && knockbackable == null) return;

        _targets.Add(new TargetInfo
        {
            Collider = collision,
            Damageable = damageable,
            Knockbackable = knockbackable
        });

        if (!_isAttacking)
            AttackLoop(_cts.Token).Forget();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < _targets.Count; i++)
        {
            if (_targets[i].Collider != collision) continue;
            
            _targets.RemoveAt(i);
            break;
        }
    }

    private async UniTaskVoid AttackLoop(CancellationToken token)
    {
        _isAttacking = true;
        int attackCooldownMs = (int)(_attackCooldown * 1000);

        try
        {
            while (_targets.Count > 0)
            {
                
                for (int i = _targets.Count - 1; i >= 0; i--)
                {
                    var target = _targets[i];

                    if (target.Collider is null)
                    {
                        _targets.RemoveAt(i);
                        continue;
                    }

                    if (target.Damageable != null)
                    {
                        var damage = Random.Range(_minDamage, _maxDamage + 1);
                        target.Damageable.TakeDamage(new DamageInfo(damage, false));
                    }

                    if (target.Knockbackable is not null)
                    {
                        Vector2 pushDir = ((Vector2)target.Collider.transform.position - (Vector2)transform.position).normalized;
                        target.Knockbackable.ApplyKnockback(pushDir * _knockbackForce);
                    }
                }

                OnTakeDamage?.Invoke();
                await UniTask.Delay(attackCooldownMs, cancellationToken: token);
            }
        }
        catch (OperationCanceledException) { }
        finally
        {
            _isAttacking = false;
        }
    }

    private void OnDisable()
    {
        _cts.Cancel();
        _cts.Dispose();
        _targets.Clear();
    }
}

