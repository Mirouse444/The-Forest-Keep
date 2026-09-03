using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInputController))]
public class GroundMover : MonoBehaviour
{ 
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _actionBuffer;

    [Space]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.5f, 0.1f);
    
    private Rigidbody2D _rigidbody;
    private PlayerInputController _controller;

    private float _currentBufferTime;
    private bool _isGrounded;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _controller = GetComponent<PlayerInputController>();
    }

    private void Update()
    {
        _currentBufferTime += Time.deltaTime;

        if (_controller.JumpPressed)
            _currentBufferTime = 0;

        if (_controller.JumpReleased && _rigidbody.linearVelocity.y > 0f)
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.y * 0.5f);
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapBox(_groundCheck.position, _groundCheckSize, 0f, _groundLayer);

        _rigidbody.linearVelocity = new Vector2(Math.Sign(_controller.MoveHorizontal) * _moveSpeed, _rigidbody.linearVelocity.y);

        if (_currentBufferTime < _actionBuffer && _isGrounded)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpForce);
            _currentBufferTime = _actionBuffer;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_groundCheck.position, _groundCheckSize);
    }
}