using UnityEngine;

public class TowerOperatorAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TargetScanner _towerCenterScanner;
    [SerializeField] private CannonStation _leftStation;
    [SerializeField] private CannonStation _rightStation;
    [SerializeField] private TowerOperatorMovement _movement;

    [Header("AI Settings")]
    [Tooltip("Ширина мертвой зоны по центру. Враг должен выйти за нее, чтобы оператор сменил пушку.")]
    [SerializeField] private float _deadZoneWidth = 3f;
    [SerializeField] private float _deadZoneHeight = 5f;

    private CannonStation _currentStation;

    private void Start()
    {
        _currentStation = _leftStation;
    }

    private void Update()
    {
        ITarget target = _towerCenterScanner.CurrentTarget;

        if (target == null) return;
        
        CannonStation requiredStation = SelectStationForTarget(target.Position);
        
        bool hasArrived = _movement.MoveTo(requiredStation.StandPosition);

        if (hasArrived)
        {
            _currentStation = requiredStation;
            OperateCannon(target);
        }
    }

    private CannonStation SelectStationForTarget(Vector3 targetPosition)
    {
        float towerCenterX = _towerCenterScanner.transform.position.x;
        float halfDeadZone = _deadZoneWidth / 2f;
        
        if (_currentStation == _leftStation && targetPosition.x > towerCenterX + halfDeadZone)
            return _rightStation;
      
        else if (_currentStation == _rightStation && targetPosition.x < towerCenterX - halfDeadZone)
            return _leftStation;

        return _currentStation;
    }

    private void OperateCannon(ITarget target)
    {
        CannonController cannon = _currentStation.Cannon;
        
        bool isAimed = cannon.AimAt(target.Position);

        if (isAimed)
        {
            cannon.TryFire();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_towerCenterScanner != null)
        {
            Vector3 center = _towerCenterScanner.transform.position;
            Vector3 size = new Vector3(_deadZoneWidth, _deadZoneHeight, 0f);
            
            Gizmos.color = new Color(1f, 0.5f, 0f, 0.3f);
            Gizmos.DrawCube(center, size);
            
            Gizmos.color = Color.red; 
            Gizmos.DrawWireCube(center, size);
        }
    }
}