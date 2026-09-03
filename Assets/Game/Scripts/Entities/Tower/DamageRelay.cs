using UnityEngine;

public class DamageRelay : MonoBehaviour, IDamageable 
{
    [SerializeField] private GameObject _mainTarget;
    
    private IDamageable _mainHealth;

    private void Awake() => _mainHealth = _mainTarget.GetComponent<IDamageable>();

    public void TakeDamage(DamageInfo info) => _mainHealth.TakeDamage(info);
}