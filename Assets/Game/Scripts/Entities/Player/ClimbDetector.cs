using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ClimbDetector : MonoBehaviour
{
    public event Action OnLadderEnter;
    public event Action OnLadderExit;
    
    public bool IsNearLadder { get; private set; }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ladder"))
        {
            IsNearLadder = true;
            OnLadderEnter?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Ladder"))
        {
            IsNearLadder = false;
            OnLadderExit?.Invoke();
        }
    }
}