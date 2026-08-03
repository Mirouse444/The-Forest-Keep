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
    
    private Rigidbody2D _rigidbody;
    private TargetScanner _scanner;
    private WaitForSeconds _jumpWait;
    private WaitForSeconds _fallCheckWait;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _scanner = GetComponent<TargetScanner>();
        _jumpWait = new WaitForSeconds(_jumpDelay);
        _fallCheckWait = new WaitForSeconds(_checkInterval);
    }

    private void OnEnable() => StartCoroutine(SlimeMove());

    private IEnumerator SlimeMove()
    { 
        while (true)
        {
            Vector2 checkPosition = (Vector2)transform.position + _groundCheckOffset;
            bool isGrounded = Physics2D.OverlapBox(checkPosition, _groundCheckSize, 0f, _groundLayer);

            if(isGrounded)
            {
                yield return _jumpWait;

                int directionX;

                if (_scanner.CurrentTarget != null)
                    directionX = (int)Mathf.Sign(_scanner.CurrentTarget.Position.x - transform.position.x);
                else
                    directionX = (int)Mathf.Sign(_mainTarget.position.x - transform.position.x);

                _rigidbody.linearVelocity = new Vector2(directionX * _horizontalSpeed, _jumpForce);
            }
            else
            {
                yield return _fallCheckWait;
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector2 checkPosition = (Vector2)transform.position + _groundCheckOffset;
        Gizmos.DrawWireCube(checkPosition, _groundCheckSize);
    }
}