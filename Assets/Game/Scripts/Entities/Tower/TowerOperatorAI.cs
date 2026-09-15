using UnityEngine;

public class TowerOperatorAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TargetScanner _scanner;
    [SerializeField] private CannonStation _leftStation;
    [SerializeField] private CannonStation _rightStation;
    [SerializeField] private TowerOperatorMovement _movement;

    [Header("AI Settings")]
    [SerializeField] private Vector2 _deadZoneSize = new Vector2(3f, 5f);
    [SerializeField] private Vector2 _deadZoneOffset = Vector2.zero;

    private CannonStation _currentStation;
    private ITarget _currentTarget;

    private void Start() => _currentStation = _leftStation;

    private void Update()
    {
        if (!IsTargetValid(_currentTarget))
            _currentTarget = _scanner.GetClosestTarget(IsTargetValid);

        if (_currentTarget == null) return;

        CannonStation requiredStation = SelectStationForTarget(_currentTarget.Position);
        
        bool hasArrived = _movement.MoveTo(requiredStation.StandPosition);

        if (hasArrived)
        {
            _currentStation = requiredStation;
            OperateCannon(_currentTarget);
        }
    }

    private bool IsTargetValid(ITarget target)
    {
        if (target == null) return false;

        if (target is Component component && (!component || !component.gameObject.activeInHierarchy))
            return false;

        return !IsInDeadZone(target.Position);
    }

    private bool IsInDeadZone(Vector3 targetPosition)
    {
        Vector2 center = (Vector2)_scanner.transform.position + _deadZoneOffset;
        float halfWidth = _deadZoneSize.x * 0.5f;
        float halfHeight = _deadZoneSize.y * 0.5f;

        bool insideX = targetPosition.x >= center.x - halfWidth && targetPosition.x <= center.x + halfWidth;
        bool insideY = targetPosition.y >= center.y - halfHeight && targetPosition.y <= center.y + halfHeight;

        return insideX && insideY;
    }

    private CannonStation SelectStationForTarget(Vector3 targetPosition)
    {
        float towerCenterX = _scanner.transform.position.x + _deadZoneOffset.x;
        float halfDeadZoneX = _deadZoneSize.x * 0.5f;

        if (_currentStation == _leftStation && targetPosition.x > towerCenterX + halfDeadZoneX)
            return _rightStation;

        if (_currentStation == _rightStation && targetPosition.x < towerCenterX - halfDeadZoneX)
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
        else
        {
            _currentTarget = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_scanner == null) return;

        Vector3 center = _scanner.transform.position + (Vector3)_deadZoneOffset;
        Vector3 size = new Vector3(_deadZoneSize.x, _deadZoneSize.y, 0f);
        
        Gizmos.color = new Color(1f, 0f, 0f, 0.25f);
        Gizmos.DrawCube(center, size);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
}