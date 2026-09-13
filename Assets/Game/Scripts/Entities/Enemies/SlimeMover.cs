using System;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

[RequireComponent(typeof(Rigidbody2D), typeof(TargetScanner))]
public class SlimeMover : MonoBehaviour
{
    [Header("Slime behaviour")]
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] private float _jumpForceRange = 0.5f;
    [SerializeField] private float _horizontalSpeed = 3f;
    [SerializeField] private float _horizontalSpeedRange = 0.5f;
    [SerializeField] private float _jumpDelay = 1.5f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Vector2 _groundCheckOffset = new Vector2(0f, -0.5f);
    [SerializeField] private Vector2 _groundCheckSize;
    [SerializeField] private float _checkInterval = 0.1f;
    
    [Header("Main Target")]
    [SerializeField] private Transform _mainTarget;

    [Header("Settings")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private TargetScanner _scanner;
    
    private float _currentJumpForce;
    private float _currentHorizontalSpeed;

    private CancellationTokenSource _cts;
    
    public event Action OnJump;

    private void OnEnable()
    {
        _currentJumpForce = _jumpForce + UnityEngine.Random.Range(-_jumpForceRange, _jumpForceRange);
        _currentHorizontalSpeed = _horizontalSpeed + UnityEngine.Random.Range(-_horizontalSpeedRange, _horizontalSpeedRange);
        
        _cts = new CancellationTokenSource();
        SlimeMove(_cts.Token).Forget();
    }

    private void OnDisable()
    {
        _cts.Cancel();
        _cts.Dispose();
    }
    
    private async UniTaskVoid SlimeMove(CancellationToken token)
    {
        int jumpDelayMs = (int)(_jumpDelay * 1000);
        int checkIntervalMs = (int)(_checkInterval * 1000);

        try
        {
            while (true) 
            {
                if (IsGrounded())
                {
                    await UniTask.Delay(jumpDelayMs, cancellationToken: token);

                    if (!IsGrounded()) continue;
                    
                    Jump();
                    OnJump?.Invoke();
                }
                else
                {
                    await UniTask.Delay(checkIntervalMs, cancellationToken: token);
                }
            }
        }
        catch (OperationCanceledException) { }
    }
    
    private bool IsGrounded()
    {
        Vector2 checkPosition = (Vector2)transform.position + _groundCheckOffset;
        return Physics2D.OverlapBox(checkPosition, _groundCheckSize, 0f, _groundLayer);
    }
    
    private void Jump()
    {
        Vector2 targetPosition = _scanner.CurrentTarget?.Position ?? (Vector2)_mainTarget.position;
        int directionX = targetPosition.x >= transform.position.x ? 1 : -1;

        _rigidbody.linearVelocity = new Vector2(directionX * _currentHorizontalSpeed, _currentJumpForce);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector2 checkPosition = (Vector2)transform.position + _groundCheckOffset;
        Gizmos.DrawWireCube(checkPosition, _groundCheckSize);
    }
}
