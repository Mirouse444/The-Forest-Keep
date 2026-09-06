using TMPro;
using System;
using UnityEngine;

public class UIMagazineCounter : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI _magazineText;
   [SerializeField] private GameObject _magazineImage;
   [SerializeField] private ReloadUIDataSO _reloadUIDataSo;
   
   private IReloadUI _playerMagazine;

   private void OnEnable() => _reloadUIDataSo.OnWeaponChanged += InitMagazine;
   private void OnDisable() => _reloadUIDataSo.OnWeaponChanged -= InitMagazine;

   private void InitMagazine()
   {
      RemoveEvent();
      
      if (_reloadUIDataSo.ReloadUI != null)
      {
         _reloadUIDataSo.ReloadUI.OnAmmoChanged += ChangeText;
         ChangeText(_reloadUIDataSo.ReloadUI.CurrentAmmo, _reloadUIDataSo.ReloadUI.MaxAmmo);
      }
      else
         CloseImage();
      
      _playerMagazine = _reloadUIDataSo.ReloadUI;
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
      
      Span<char> buffer = stackalloc char[32];
      int length = 0;
      
      if (currentProjectile.TryFormat(buffer.Slice(length), out int written))
         length += written;
      
      buffer[length++] = '/';
      
      if (maxProjectile.TryFormat(buffer.Slice(length), out written))
         length += written;
      
      _magazineText.SetText(buffer.Slice(0, length));
   }
}