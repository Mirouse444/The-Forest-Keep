using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EquipButton : MonoBehaviour
{
    [SerializeField] private Transform _playerWeaponHolder;
    [SerializeField] private GameObject[] _otherEquipButtons;
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private WeaponDataUISO _dataUiso;
    [SerializeField] private  Button _button;


    private void OnEnable() => _button.onClick.AddListener(Equip);

    private void OnDisable() => _button.onClick.RemoveListener(Equip);

    private void Equip()
    {
        foreach (var button in _otherEquipButtons)
            button.SetActive(true); 
        
        _inventoryUI.UpdateWeaponIcon(_dataUiso.UIIcon, _dataUiso.Category);

        GameObject weapon = Instantiate(_dataUiso.WeaponPrefab, _playerWeaponHolder);
        weapon.transform.localPosition = Vector3.zero;
        
        weapon.SetActive(false);
        _inventory.AddWeapon(weapon, _dataUiso.Category);
        
        gameObject.SetActive(false);
    }
}