using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EquipButton : MonoBehaviour
{
    [SerializeField] private Transform _playerWeaponHolder;
    [SerializeField] private GameObject[] _otherEquipButtons;
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private WeaponDataUI _dataUI;

    private  Button _button;
    
    private void Awake() => _button = GetComponent<Button>();

    private void OnEnable() => _button.onClick.AddListener(Equip);

    private void OnDisable() => _button.onClick.RemoveListener(Equip);

    private void Equip()
    {
        foreach (var button in _otherEquipButtons)
            button.SetActive(true); 
        
        _inventoryUI.UpdateWeaponIcon(_dataUI.UIIcon, _dataUI.Category);

        GameObject weapon = Instantiate(_dataUI.WeaponPrefab, _playerWeaponHolder);
        weapon.transform.localPosition = Vector3.zero;
        
        weapon.SetActive(false);
        _inventory.AddWeapon(weapon, _dataUI.Category);
        
        gameObject.SetActive(false);
    }
}