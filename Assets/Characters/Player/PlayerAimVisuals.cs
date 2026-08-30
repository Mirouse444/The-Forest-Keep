using UnityEngine;

public class PlayerAimVisuals : MonoBehaviour
{
    private const float Offset = 0.07f; 
    
    [SerializeField] private PlayerAim _aimProvider;
    [SerializeField] private Transform _weaponHolder;
    
    [Header("Settings")]
    [SerializeField, Range(0f, 90f)] private float _maxAngleUp = 30f;
    [SerializeField, Range(0f, 90f)] private float _maxAngleDown = 30f;
    [SerializeField, Range(5f, 30f)] private float _weaponRotationSpeed = 15f;
    
    private SpriteRenderer _PlayerSpriteRenderer;
    private Vector3 _rightWeaponLocalPosition;
    private Vector3 _leftWeaponLocalPosition;
    private Vector3 _rightWeaponScale;
    private Vector3 _leftWeaponScale;
    private bool _isFacingRight;

    private void Awake() => _PlayerSpriteRenderer = GetComponent<SpriteRenderer>();

    private void Start()
    {
        _rightWeaponLocalPosition = new Vector3(-_weaponHolder.localPosition.x, _weaponHolder.localPosition.y, _weaponHolder.localPosition.z);
        _leftWeaponLocalPosition =  _weaponHolder.localPosition;
        _rightWeaponScale = new  Vector3(_weaponHolder.localScale.x, -_weaponHolder.localScale.y, _weaponHolder.localScale.z);
        _leftWeaponScale =  _weaponHolder.localScale;
    }
    
    private void Update()
    {
        Vector2 flipDirection = _aimProvider.GetAimDirection(transform.position);
        
        switch (flipDirection.x)
        {
            case < -Offset:
                _PlayerSpriteRenderer.flipX = true;
                _weaponHolder.localPosition = _rightWeaponLocalPosition;
                _weaponHolder.localScale = _rightWeaponScale;
                _isFacingRight = false;
                break;
            case > Offset:
                _PlayerSpriteRenderer.flipX = false;
                _weaponHolder.localPosition = _leftWeaponLocalPosition;
                _weaponHolder.localScale = _leftWeaponScale;
                _isFacingRight = true;
                break;
        }

        Vector2 weaponAimDirection = _aimProvider.GetAimDirection(_weaponHolder.position);
        
        Vector2 workingDirection = new Vector2(Mathf.Abs(weaponAimDirection.x), weaponAimDirection.y);
        float targetAngle = Mathf.Atan2(workingDirection.y, workingDirection.x) * Mathf.Rad2Deg;
        targetAngle = Mathf.Clamp(targetAngle, -_maxAngleDown, _maxAngleUp);

        if (!_isFacingRight)
            targetAngle = 180f - targetAngle;
        
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        
        _weaponHolder.rotation = Quaternion.Lerp(
            _weaponHolder.rotation, 
            targetRotation, 
            Time.deltaTime * _weaponRotationSpeed
        );
    }
}