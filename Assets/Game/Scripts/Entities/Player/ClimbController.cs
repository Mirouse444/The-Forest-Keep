using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
[RequireComponent(typeof(Rigidbody2D))]
public class ClimbController : MonoBehaviour
{
    [SerializeField] private float _climbSpeed = 5f;

    private PlayerInputController _controller;
    private Rigidbody2D _rigidbody;
    
    private float _defaultGravity;
    private int _climbingLayer;
    private int _playerLayer;
    private bool _isClimbing;

    private void Awake()
    {
        _controller = GetComponent<PlayerInputController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        
        _defaultGravity = _rigidbody.gravityScale;
        _playerLayer = LayerMask.NameToLayer("Player");
        _climbingLayer = LayerMask.NameToLayer("PlayerClimbing");
    }

    private void Update()
    {
        float verticalInput = _controller.MoveVertical;

        if (!_isClimbing && Mathf.Abs(verticalInput) > 0.1f)
            StartClimbing();

        if (_isClimbing)
            _rigidbody.linearVelocity = new Vector2(0, verticalInput * _climbSpeed);
    }

    private void StartClimbing()
    {
        _isClimbing = true;
        _rigidbody.gravityScale = 0f;
        _rigidbody.linearVelocity = Vector2.zero;
        gameObject.layer = _climbingLayer; 
    }

    public void StopClimbing()
    {
        if (!_isClimbing) return;
        
        _isClimbing = false;
        _rigidbody.gravityScale = _defaultGravity;
        gameObject.layer = _playerLayer;
    }
}