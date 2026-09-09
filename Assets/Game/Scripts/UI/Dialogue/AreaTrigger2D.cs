using UnityEngine;
using UnityEngine.Events;

public class AreaTrigger2D : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private bool _oneTimeTrigger = true;
    
    public UnityEvent OnEntered;

    private bool _hasTriggered;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_oneTimeTrigger && _hasTriggered) return;

        if (_targetLayer.Contains(collider.gameObject.layer))
        {
            _hasTriggered = true;
            OnEntered?.Invoke();
        }
    }
}