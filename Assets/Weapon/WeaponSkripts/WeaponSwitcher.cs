using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private PlayerWeaponController _weaponController;
    [SerializeField] private PlayerInputController _input;
    [SerializeField] private Inventory _inventory;

    private void OnEnable()
    {
        _input.OnButton1 += Button1Pressed;
        _input.OnButton2 += Button2Pressed;
        _input.OnButton3 += Button3Pressed;
        _input.OnButton4 += Button4Pressed;
    }

    private void OnDisable()
    {
        _input.OnButton1 -= Button1Pressed;
        _input.OnButton2 -= Button2Pressed;
        _input.OnButton3 -= Button3Pressed;
        _input.OnButton4 -= Button4Pressed;
    }

    private void Button1Pressed() => SelectCategory(WeaponCategory.MeleeWeapon);
    private void Button2Pressed() => SelectCategory(WeaponCategory.Shotgun);
    private void Button3Pressed() => SelectCategory(WeaponCategory.AssaultRifle);
    private void Button4Pressed() => SelectCategory(WeaponCategory.SniperRifle);
    
    private void SelectCategory(WeaponCategory category)
    {
        GameObject weapon = _inventory.GetWeapon(category);
        
        if(weapon != null)
        {
            weapon.SetActive(true);
            _weaponController.EquipWeapon(weapon);
        }
    }
}

