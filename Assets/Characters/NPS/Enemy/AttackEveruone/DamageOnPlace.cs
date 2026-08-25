using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageOnPlace : MonoBehaviour
{
    [SerializeField] private int _minDamage = 10;
    [SerializeField] private int _maxDamage = 15;
    [SerializeField] private float _knockbackForce = 5f;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private LayerMask _targetLayer;

    private IDamageTextSpawner _textSpawner;
    

    private struct TargetInfo
    {
        public Collider2D Collider;
        public IDamageable Damageable;
        public IKnockbackable Knockbackable;
    }

    private readonly List<TargetInfo> _targets = new();
    
    private Coroutine _attackCoroutine;
    private WaitForSeconds _cooldown;
    
    public event System.Action OnTakeDamage;

    private void Awake() => _cooldown = new WaitForSeconds(_attackCooldown);
    
    public void Initialize(IDamageTextSpawner textSpawner) => _textSpawner = textSpawner;

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

        _attackCoroutine ??= StartCoroutine(AttackLoop());
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

    private IEnumerator AttackLoop()
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

                if(target.Damageable != null)
                {
                    var damage = Random.Range(_minDamage, _maxDamage + 1);
                    target.Damageable.TakeDamage(damage);
                    _textSpawner?.SpawnText(target.Collider.transform.position, damage, false);
                }

                if (target.Knockbackable is not null)
                {
                    Vector2 pushDir = ((Vector2)target.Collider.transform.position - (Vector2)transform.position).normalized;
                    target.Knockbackable.ApplyKnockback(pushDir * _knockbackForce);
                }
            }

            OnTakeDamage?.Invoke();
            yield return _cooldown;
        }

        _attackCoroutine = null;
    }

    private void OnDisable()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }

        _targets.Clear();
    }
}