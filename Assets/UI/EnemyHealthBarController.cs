using System;
using UnityEngine;

public class EnemyHealthBarController : MonoBehaviour
{
    [SerializeField] private Transform _HPLinePosition;

    private Action<HPLine> _releaseAction;
    private HealthComponent _state;
    private HPLine _hPLine;

    private void Awake() => _state = GetComponent<HealthComponent>();

    public void Initialize(HPLine line, Action<HPLine> releaseToPool)
    {
        _hPLine = line;
        _hPLine.ChangeState(_state.State);
        _releaseAction = releaseToPool;
    }

    private void LateUpdate() => _hPLine.SetScreenPosition(_HPLinePosition.position);

    private void OnDisable() => _releaseAction.Invoke(_hPLine);
}
