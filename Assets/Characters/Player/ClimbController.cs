using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ClimbDetector))]
public class ClimbController : MonoBehaviour
{
    [SerializeField] private float _climbSpeed = 5f;

    private ClimbDetector _detector;
    private Rigidbody2D _rigidbody;
    
    private float _defaultGravity;
    private int _climbingLayer;
    private int _playerLayer;
    private bool _isClimbing;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _detector = GetComponent<ClimbDetector>();
        
        _defaultGravity = _rigidbody.gravityScale;
        _playerLayer = LayerMask.NameToLayer("Player");
        _climbingLayer = LayerMask.NameToLayer("PlayerClimbing");
    }

    public void HandleClimbInput(Vector2 input)
    {
        // Если мы рядом с лестницей и нажали Вверх/Вниз, а до этого не карабкались
        if (_detector.IsNearLadder && Mathf.Abs(input.y) > 0.1f && !_isClimbing)
        {
            StartClimbing();
        }

        // Если мы ушли с лестницы ИЛИ нажали прыжок
        if (!_detector.IsNearLadder && _isClimbing)
        {
            StopClimbing();
        }

        // Сама логика движения, если мы в состоянии карабканья
        if (_isClimbing)
        {
            _rigidbody.linearVelocity = new Vector2(0, input.y * _climbSpeed);
            
            // Опционально: можно добавить центрирование по X, 
            // чтобы игрок ровно висел на лестнице:
            // transform.position = new Vector3(_detector.CurrentLadder.position.x, transform.position.y, transform.position.z);
        }
    }

    private void StartClimbing()
    {
        _isClimbing = true;
        _rigidbody.gravityScale = 0f; // Выключаем гравитацию
        _rigidbody.linearVelocity = Vector2.zero; // Сбрасываем текущую инерцию
        
        // Меняем слой, чтобы игрок начал проходить сквозь платформы (пол)
        gameObject.layer = _climbingLayer; 
    }

    public void StopClimbing()
    {
        _isClimbing = false;
        _rigidbody.gravityScale = _defaultGravity; // Возвращаем обычную гравитацию
        
        gameObject.layer = _playerLayer; // Снова сталкиваемся с полом
    }
}