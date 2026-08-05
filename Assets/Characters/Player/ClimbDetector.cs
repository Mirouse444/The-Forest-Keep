using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ClimbDetector : MonoBehaviour
{
    public event Action<Transform> OnLadderEnter;
    public event Action OnLadderExit;
    
    public bool IsNearLadder { get; private set; }
    public Transform CurrentLadder { get; private set; }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ladder"))
        {
            IsNearLadder = true;
            CurrentLadder = col.transform;
            OnLadderEnter?.Invoke(CurrentLadder);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Ladder"))
        {
            IsNearLadder = false;
            CurrentLadder = null;
            OnLadderExit?.Invoke();
        }
    }
}