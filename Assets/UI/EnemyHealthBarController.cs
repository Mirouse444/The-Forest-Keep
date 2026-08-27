using System;
using UnityEngine;

public class EnemyHealthBarController : MonoBehaviour
{
    [SerializeField] private HPLineSO _hPLineSO;
    [SerializeField] private Transform _HPLinePosition;
    [SerializeField] private HealthComponent _state;

    private Action<HPLine> _releaseAction;
    private HPLine _hPLine;

    private void OnEnable()
    {
        _hPLine = _hPLineSO.HPLineSpawner.GetHPLine(out _releaseAction);
        _hPLine.ChangeState(_state.State);
    }
    private void OnDisable() => _releaseAction.Invoke(_hPLine);

    private void LateUpdate() => _hPLine.SetScreenPosition(_HPLinePosition.position);

}
