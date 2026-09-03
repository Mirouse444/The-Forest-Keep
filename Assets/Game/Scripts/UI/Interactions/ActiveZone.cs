using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ActiveZone : MonoBehaviour
{
    [SerializeField] private GameObject _trigger;
    
    public bool PlayerInZone {get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject ==  _trigger)
            PlayerInZone = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == _trigger)
            PlayerInZone = false;
    }
}
