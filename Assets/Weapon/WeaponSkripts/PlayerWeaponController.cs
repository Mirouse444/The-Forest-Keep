using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerAim))]
[RequireComponent(typeof(PlayerInputReader))]
public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private UIMagazineCounter  _magazineCounter;
    [SerializeField] private UIMagazineReload _reload;
    
    private IReloadMagazine _currentMagazine;
    private PlayerInputReader _inputReader;
    private IWeaponTrigger _currentWeapon;
    
    private bool _isPointerOverUI;

    private void Awake() => _inputReader = GetComponent<PlayerInputReader>();
    

    private void OnEnable()
    {
        _inputReader.OnFireStarted += FirePressed;
        _inputReader.OnFireCanceled += FireReleased;
        _inputReader.OnReloadStarted += ReloadPressed;
    }

    private void OnDisable()
    {
        _inputReader.OnFireStarted -= FirePressed;
        _inputReader.OnFireCanceled -= FireReleased;
        _inputReader.OnReloadStarted -= ReloadPressed;
    }

    private void Update() => _isPointerOverUI = EventSystem.current.IsPointerOverGameObject();

    public void EquipWeapon(GameObject weaponObject)
    {
        if (weaponObject == null)
        {
            _currentWeapon = null;
            _currentMagazine = null;
            _reload.InitMagazine(null);
            _magazineCounter.InitMagazine(null);
        }
        else
        {
            weaponObject.TryGetComponent(out _currentWeapon);
            weaponObject.TryGetComponent(out _currentMagazine);
            weaponObject.TryGetComponent(out IReloadUI uiInfo);

            _reload.InitMagazine(uiInfo);
            _magazineCounter.InitMagazine(uiInfo);
        }
    }

    private void FirePressed()
    {
        if (_isPointerOverUI)
            return;
        
        if (_currentMagazine as Object == null)
            return;
        
        if (_currentMagazine is { IsEmpty: true } )  
            ReloadPressed();
        else
            _currentWeapon?.OnTriggerPressed();
    }

    private void FireReleased() => _currentWeapon?.OnTriggerReleased();
    
    private void ReloadPressed() => _currentMagazine?.Reload();
}