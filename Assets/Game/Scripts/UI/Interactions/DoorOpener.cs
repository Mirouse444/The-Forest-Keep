using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private int _openDoorLayer;
    [SerializeField] private int _closeDoorLayer;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _playerLayer;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_playerLayer.Contains(collision.gameObject.layer))
        {
            _animator.SetBool("IsOpen", true);
            _door.layer = _openDoorLayer;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_playerLayer.Contains(collision.gameObject.layer))
        {
            _animator.SetBool("IsOpen", false);
            _door.layer = _closeDoorLayer;
        }
    }
}
