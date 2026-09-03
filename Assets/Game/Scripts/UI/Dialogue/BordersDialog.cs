using UnityEngine;

public class BordersDialog : MonoBehaviour
{
    [SerializeField] private GameObject _dialog;
    [SerializeField] private LayerMask _playerLayer;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(_playerLayer.Contains(collider.gameObject.layer))
        {
            _dialog.SetActive(true);
            Time.timeScale = 0;
        }
    }
}