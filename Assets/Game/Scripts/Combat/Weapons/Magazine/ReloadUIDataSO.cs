using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/MagazineSO", fileName = "MagazineSO", order = 0)]
public class ReloadUIDataSO : ScriptableObject
{
    private IReloadUI _reloadUI;
    public event System.Action OnWeaponChanged;

    public IReloadUI ReloadUI 
    { 
        get => _reloadUI; 
        set 
        {
            _reloadUI = value;
            OnWeaponChanged?.Invoke();
        }
    }
}