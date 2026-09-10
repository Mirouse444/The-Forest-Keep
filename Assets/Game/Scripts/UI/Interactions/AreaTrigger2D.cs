using UnityEngine;
using UnityEngine.Events;

public class AreaTrigger2D : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    
    public UnityEvent OnEntered;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_targetLayer.Contains(collider.gameObject.layer))
            OnEntered?.Invoke(); 
    }
}