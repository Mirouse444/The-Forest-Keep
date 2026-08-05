using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _playerLayer;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_playerLayer.Contains(collision.gameObject.layer))
            _animator.SetBool("IsOpen", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_playerLayer.Contains(collision.gameObject.layer))
            _animator.SetBool("IsOpen", false);
    }
}
