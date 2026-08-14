using UnityEngine;

public class SemiAutomatic : MonoBehaviour, IWeaponTrigger
{
    [SerializeField] private float _fireRate = 0.1f;

    private IGunLauncher _launcher;
    private float _timer;

    private void Awake() => _launcher = GetComponent<IGunLauncher>();

    public void OnTriggerPressed(IAimProvider aimProvider)
    {
        if (Time.time - _timer > _fireRate)
        {
            _timer = Time.time;
            _launcher.Fire(aimProvider.GetAimDirection(_launcher.transform.position));
        }
    }
    
    public void OnTriggerReleased(IAimProvider aimProvider) {}
}