using UnityEngine;

[RequireComponent(typeof(PlayerAim))]
[RequireComponent(typeof(PlayerInputReader))]
public class PlayerWeaponController : MonoBehaviour
{
    private PlayerInputReader _inputReader;
    private IWeaponTrigger _currentWeapon;
    private IAimProvider _aimProvider;

    private void Awake()
    {
        _inputReader = GetComponent<PlayerInputReader>();
        _aimProvider = GetComponent<PlayerAim>();

        _inputReader.OnFireStarted += FirePressed;
        _inputReader.OnFireCanceled += FireReleased;
    }

    public void EquipWeapon(GameObject weaponObject) => _currentWeapon = weaponObject.GetComponent<IWeaponTrigger>();

    private void FirePressed() => _currentWeapon?.OnTriggerPressed(_aimProvider);
    private void FireReleased() => _currentWeapon?.OnTriggerReleased(_aimProvider);
}