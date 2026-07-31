using UnityEngine;

[RequireComponent(typeof(PlayerInputReader))]
[RequireComponent(typeof(PlayerAim))]
public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private GameObject _startingWeaponPrefab;

    private PlayerInputReader _inputReader;
    private IAimProvider _aimProvider;
    private IWeaponTrigger _currentWeapon;

    private void Awake()
    {
        _inputReader = GetComponent<PlayerInputReader>();
        _aimProvider = GetComponent<PlayerAim>();

        _inputReader.OnFireStarted += FirePressed;
        _inputReader.OnFireCanceled += FireReleased;
    }

    private void Start() => EquipWeapon(_startingWeaponPrefab);

    public void EquipWeapon(GameObject weaponObject)
    {
        if (weaponObject == null) return;
        _currentWeapon = weaponObject.GetComponent<IWeaponTrigger>();
    }

    private void FirePressed() => _currentWeapon?.OnTriggerPressed(_aimProvider);

    private void FireReleased() => _currentWeapon?.OnTriggerReleased(_aimProvider);
}