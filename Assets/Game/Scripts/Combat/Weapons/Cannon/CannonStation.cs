using UnityEngine;

public class CannonStation : MonoBehaviour
{
    [SerializeField] private CannonController _cannon;
    [SerializeField] private Transform _operatorStandPosition;
    [SerializeField] private bool _isLeftSide;

    public CannonController Cannon => _cannon;
    public Vector3 StandPosition => _operatorStandPosition.position;
    public bool IsLeftSide => _isLeftSide;
}