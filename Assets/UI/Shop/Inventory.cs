using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   private readonly Dictionary<WeaponCategory, GameObject> _ownedWeapons = new Dictionary<WeaponCategory, GameObject>();

   public void AddWeapon(GameObject weapon, WeaponCategory category)
   {
      if (_ownedWeapons.TryGetValue(category, out GameObject oldWeapon))
         Destroy(oldWeapon);
      
      _ownedWeapons[category] = weapon;
   }

   public GameObject GetWeapon(WeaponCategory category) => _ownedWeapons.GetValueOrDefault(category);
}