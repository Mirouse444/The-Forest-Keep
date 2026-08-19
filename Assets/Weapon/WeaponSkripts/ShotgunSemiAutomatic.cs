using UnityEngine;

public class ShotgunSemiAutomatic : MonoBehaviour, IWeaponTrigger
{
    [SerializeField, Min(0)] private float _fireRate = 0.1f;
    [SerializeField, Min(1)] private int _projectileCount;
    [SerializeField, Range(0, 180)] private float _spreadAngle = 30f;

    
    private IWeaponLauncher _weaponLauncher;
    private IWeaponMagazine _magazine;
    private float _timer;

    private void Awake()
    {
        _weaponLauncher = GetComponent<IWeaponLauncher>();
        _magazine = GetComponent<IWeaponMagazine>();
    }

    public void OnTriggerPressed(IAimProvider aimProvider)
    {
        if (Time.time - _timer > _fireRate)
        {
            if (!_magazine.TryConsumeAmmo()) return;
            
            _timer = Time.time;
            
            Vector2 baseDirection = aimProvider.GetAimDirection(_weaponLauncher.transform.position);
            
            float angleStep = _projectileCount > 1 ? _spreadAngle / (_projectileCount - 1) : 0f;
  
            float startAngle = _spreadAngle / 2f;

            for (int i = 0; i < _projectileCount; i++)
            {
                float currentAngleOffset = startAngle - angleStep * i;
                
                Vector2 spreadDirection = Quaternion.Euler(0, 0, currentAngleOffset) * baseDirection;
                
                _weaponLauncher.Fire(spreadDirection);
            }
        }
    }
    
    public void OnTriggerReleased(IAimProvider aimProvider) {}
}