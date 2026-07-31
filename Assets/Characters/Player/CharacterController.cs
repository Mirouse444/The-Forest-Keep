using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _actionBuffer;

    [Space]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.5f, 0.1f);

    private PlayerInput _playerInput;
    private Rigidbody2D _rigidbody;
    private KnockbackReceiver _knockbackReceiver;

    private float _currentBufferTime;
    private float _horizontalInput;
    private bool _isGrounded;
    private float _knockbackTimer; 

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = new PlayerInput();
        _knockbackReceiver = GetComponent<KnockbackReceiver>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();

        if (_knockbackReceiver != null)
            _knockbackReceiver.KnockbackApplied += OnKnockbackApplied;
    }

    private void OnDisable()
    {
        _playerInput.Disable();

        if (_knockbackReceiver != null)
            _knockbackReceiver.KnockbackApplied -= OnKnockbackApplied;
    }

    private void OnKnockbackApplied(float duration)
    {
        _knockbackTimer = duration;
    }

    private void Update()
    {
        _currentBufferTime += Time.deltaTime;

        _horizontalInput = _playerInput.Player.Move.ReadValue<Vector2>().x;

        if (_horizontalInput > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (_horizontalInput < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        if (_playerInput.Player.Jump.WasPressedThisFrame())
            _currentBufferTime = 0;

        if (_playerInput.Player.Jump.WasReleasedThisFrame() && _rigidbody.linearVelocity.y > 0f)
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.y * 0.5f);
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapBox(_groundCheck.position, _groundCheckSize, 0f, _groundLayer);

        if (_knockbackTimer > 0)
            _knockbackTimer -= Time.fixedDeltaTime;
        else
            _rigidbody.linearVelocity = new Vector2(_horizontalInput * _moveSpeed, _rigidbody.linearVelocity.y);

        if (_currentBufferTime < _actionBuffer && _isGrounded)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpForce);
            _currentBufferTime = _actionBuffer;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(_groundCheck.position, _groundCheckSize);
        }
    }
}