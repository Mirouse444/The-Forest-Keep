using UnityEngine;

public class EnemyHealthBarController : MonoBehaviour
{
    [SerializeField] private HealthComponent _state;
    [SerializeField] private HPLinesPool _pool;

    private Camera _mainCamera;
    private HPLine _hPLine;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _hPLine = _pool.GetHPLine();

        _hPLine.ChangeState(_state.State);
    }

    private void LateUpdate()
    {
        if(_hPLine)
            _hPLine.HPLineRectTransform.position = _mainCamera.WorldToScreenPoint(transform.position);
    }

    private void OnDisable()
    {
        if (_hPLine)
            _pool.ReleaseToPool(_hPLine);
    }
}
