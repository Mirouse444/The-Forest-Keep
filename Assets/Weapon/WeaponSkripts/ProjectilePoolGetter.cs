using UnityEngine;

public class ProjectilePoolGetter : MonoBehaviour
{
   [SerializeField] private ProjectilePool _simplePool;
   [SerializeField] private ProjectilePool _sniperPool;
   [SerializeField] private ProjectilePool _explosivePool;

   public ProjectilePool GetPool(ProjectileType type)
   {
      return type switch
      {
         ProjectileType.Simple => _simplePool,
         ProjectileType.Sniper => _sniperPool,
         ProjectileType.Explosive => _explosivePool,
         _ => null
      };
   }
}