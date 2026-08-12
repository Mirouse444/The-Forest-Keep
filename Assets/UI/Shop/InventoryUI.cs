using UnityEngine.UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("Слоты в интерфейсе")]
    [SerializeField] private Image _meleeSlotIcon;
    [SerializeField] private Image _shotgunSlotIcon;
    [SerializeField] private Image _assaultRifleSlotIcon;
    [SerializeField] private Image _sniperRifleSlotIcon;
    
    public void UpdateWeaponIcon(Sprite icon, WeaponCategory category)
    {
        switch (category)
        {
            case WeaponCategory.MeleeWeapon:
                SetSprite(_meleeSlotIcon, icon);
                break;
                
            case WeaponCategory.Shotgun:
                SetSprite(_shotgunSlotIcon, icon);
                break;
                
            case WeaponCategory.AssaultRifle:
                SetSprite(_assaultRifleSlotIcon, icon);
                break;
                
            case WeaponCategory.SniperRifle:
                SetSprite(_sniperRifleSlotIcon, icon);
                break;
        }
    }

    private void SetSprite(Image icon, Sprite weapon)
    {
        icon.sprite = weapon;
        icon.enabled = true;
    }
}