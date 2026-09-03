using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private PlayerWeaponController _weaponController;
    [SerializeField] private PlayerInputController _input;
    [SerializeField] private Inventory _inventory;

    [SerializeField] private Button[]  _buttons = new Button[4]; 
    private GameObject _currentActiveWeapon;
    
    private void OnEnable()
    {
        _input.OnButton1 += Button1Pressed;
        _buttons[0].onClick.AddListener(Button1Pressed);
        _input.OnButton2 += Button2Pressed;
        _buttons[1].onClick.AddListener(Button2Pressed);
        _input.OnButton3 += Button3Pressed;
        _buttons[2].onClick.AddListener(Button3Pressed);
        _input.OnButton4 += Button4Pressed;
        _buttons[3].onClick.AddListener(Button4Pressed);
    }

    private void OnDisable()
    {
        _input.OnButton1 -= Button1Pressed;
        _buttons[0].onClick.RemoveListener(Button1Pressed);
        _input.OnButton2 -= Button2Pressed;
        _buttons[1].onClick.RemoveListener(Button2Pressed);
        _input.OnButton3 -= Button3Pressed;
        _buttons[2].onClick.RemoveListener(Button3Pressed);
        _input.OnButton4 -= Button4Pressed;
        _buttons[3].onClick.RemoveListener(Button4Pressed);
    }

    private void Button1Pressed() => SelectCategory(WeaponCategory.AssaultRifle);
    private void Button2Pressed() => SelectCategory(WeaponCategory.Shotgun);
    private void Button3Pressed() => SelectCategory(WeaponCategory.SniperRifle);
    private void Button4Pressed() => SelectCategory(WeaponCategory.Spacial);
    
    private void SelectCategory(WeaponCategory category)
    {
        if (_currentActiveWeapon != null)
            _currentActiveWeapon.SetActive(false);
        
        _currentActiveWeapon = _inventory.GetWeapon(category);
        
        if(_currentActiveWeapon != null)
        {
            _currentActiveWeapon.SetActive(true);
            _weaponController.EquipWeapon(_currentActiveWeapon);
        }
        else
        {
            _weaponController.EquipWeapon(null);
        }
    }
}

