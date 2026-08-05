using UnityEngine;
using System.Collections;

public class PlayerBrain : MonoBehaviour
{
    private GroundMover _groundMover;
    private ClimbController _climbController;
    private KnockbackReceiver _knockback;
    private ClimbDetector _detector;

    private void Awake()
    {
        _groundMover = GetComponent<GroundMover>();
        _climbController = GetComponent<ClimbController>();
        _knockback = GetComponent<KnockbackReceiver>();
        _detector = GetComponentInChildren<ClimbDetector>();
        
        _climbController.enabled = false; 
    }

    private void OnEnable()
    {
        _detector.OnLadderEnter += EnableClimbingPotential;
        _detector.OnLadderExit += DisableClimbingPotential;

        _knockback.KnockbackApplied += OnStunned;
    }

    private void OnDisable()
    {
        _detector.OnLadderEnter -= EnableClimbingPotential;
        _detector.OnLadderExit -= DisableClimbingPotential;
        _knockback.KnockbackApplied -= OnStunned;
    }

    private void EnableClimbingPotential() => _climbController.enabled = true;
    
    private void DisableClimbingPotential() 
    {
        _climbController.StopClimbing();
        _climbController.enabled = false;
    }
    

    private void OnStunned(float duration)
    {
        _groundMover.enabled = false;
        _climbController.StopClimbing();
        _climbController.enabled = false;
        
        StartCoroutine(RestoreControlRoutine(duration));
    }

    private IEnumerator RestoreControlRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        _groundMover.enabled = true;
        
        if (_detector.IsNearLadder)
            _climbController.enabled = true;
    }
}