using TMPro;
using UnityEngine;

public class UIMagazineCounter : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI _magazineText;
   [SerializeField] private GameObject _magazineImage;

   private IReloadUI _playerMagazine;
   
   public void InitMagazine(IReloadUI magazine)
   {
      RemoveEvent();
      
      if (magazine != null)
      {
         magazine.OnAmmoChanged += ChangeText;
         ChangeText(magazine.CurrentAmmo, magazine.MaxAmmo);
      }
      else
         CloseImage();
      
        _playerMagazine = magazine;
   }

   private void OnDestroy() => RemoveEvent();

   private void RemoveEvent()
   {
      if (_playerMagazine != null) 
         _playerMagazine.OnAmmoChanged -= ChangeText;
   }

   private void CloseImage() => _magazineImage.SetActive(false);

   private void ChangeText(int currentProjectile, int maxProjectile)
   {
      if (!_magazineImage.activeSelf) 
         _magazineImage.SetActive(true);
      
      _magazineText.SetText("{0}/{1}", currentProjectile,  maxProjectile);
   }
   
}