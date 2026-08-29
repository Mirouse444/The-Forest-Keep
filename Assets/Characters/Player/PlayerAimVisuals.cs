using UnityEngine;

public class PlayerAimVisuals : MonoBehaviour
{
    private const float Offset = 0.4f; 
    
    [SerializeField] private PlayerAim _aimProvider;
    [SerializeField] private Transform _weaponHolder;
    
    [Header("Настройки")]
    [SerializeField, Range(5f, 30f)] private float _weaponRotationSpeed = 15f;
    
    private SpriteRenderer _PlayerSpriteRenderer;
    private Vector3 _rightWeaponLocalPosition;
    private Vector3 _leftWeaponLocalPosition;
    private Vector3 _rightWeaponScale;
    private Vector3 _leftWeaponScale;

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
                break;
            case > Offset:
                _PlayerSpriteRenderer.flipX = false;
                _weaponHolder.localPosition = _leftWeaponLocalPosition;
                _weaponHolder.localScale = _leftWeaponScale;
                break;
        }

        Vector2 weaponAimDirection = _aimProvider.GetAimDirection(_weaponHolder.position);
        
        float targetAngle = Mathf.Atan2(weaponAimDirection.y, weaponAimDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        
        _weaponHolder.rotation = Quaternion.Lerp(
            _weaponHolder.rotation, 
            targetRotation, 
            Time.deltaTime * _weaponRotationSpeed
        );
    }
}