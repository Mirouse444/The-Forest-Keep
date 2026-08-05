using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TargetScanner))]

internal class SlimeMover : MonoBehaviour
{
    [Header("Slime behaviour")]
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] private float _horizontalSpeed = 3f;
    [SerializeField] private float _jumpDelay = 1.5f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Vector2 _groundCheckOffset = new Vector2(0f, -0.5f);
    [SerializeField] private Vector2 _groundCheckSize;
    [SerializeField] private float _checkInterval = 0.1f;
    
    [Header("Main Target")]
    [SerializeField] private Transform _mainTarget;

    private Coroutine _slimeMove;
    private Rigidbody2D _rigidbody;
    private TargetScanner _scanner;
    private WaitForSeconds _jumpWait;
    private WaitForSeconds _fallCheckWait;
    
    private float _currentJumpForce;
    private float _currentHorizontalSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _scanner = GetComponent<TargetScanner>();
        _jumpWait = new WaitForSeconds(_jumpDelay);
        _fallCheckWait = new WaitForSeconds(_checkInterval);
    }

    private void OnEnable()
    {
        _slimeMove = StartCoroutine(SlimeMove());
        _currentJumpForce = _jumpForce + Random.Range(-0.5f, 0.5f);
        _currentHorizontalSpeed = _horizontalSpeed + Random.Range(-0.5f, 0.5f);
    }

    private void OnDisable()
    {
        StopCoroutine(_slimeMove);
    }

    private IEnumerator SlimeMove()
    { 
        while (true)
        {
            if(IsGrounded())
            {
                yield return _jumpWait;
                
                if(IsGrounded())
                    Jump();
            }
            else
            {
                yield return _fallCheckWait;
            }

        }
    }
    
    private bool IsGrounded()
    {
        Vector2 checkPosition = (Vector2)transform.position + _groundCheckOffset;

        return Physics2D.OverlapBox(
            checkPosition,
            _groundCheckSize,
            0f,
            _groundLayer);
    }
    
    private void Jump()
    {
        Vector2 targetPosition = _scanner.CurrentTarget?.Position ?? _mainTarget.position;
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