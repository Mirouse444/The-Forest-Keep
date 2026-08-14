using System;
using UnityEngine;
using UnityEngine.UI;

public class HPLine : MonoBehaviour
{ 
    [SerializeField] private GameObject _ImageObject;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _fillImage;
    [SerializeField] private float _timeToOff = 5f;
    
    private Camera _mainCamera;
    private IHealthState _state;
    
    private float _timer;

    private void Awake() => _mainCamera = Camera.main;

    public void ChangeState(IHealthState state)
    {
        if (_state != null)
            _state.OnHealthChanged -= ChangeHp;

        _state = state;
        _state.OnHealthChanged += ChangeHp;
    }

    public void SetScreenPosition(Vector3 worldPosition) => _rectTransform.position = _mainCamera.WorldToScreenPoint(worldPosition); 
    
    private void ChangeHp(int currentHealth, int maxHealth)
    {
        if (currentHealth <= 0)
        {
            _state.OnHealthChanged -= ChangeHp;
            _ImageObject.SetActive(false);
        }
        else
        {
            _ImageObject.SetActive(true);

            _timer = 0f;
            _fillImage.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_timeToOff <= _timer)
            _ImageObject.SetActive(false);
    }

    private void OnDisable() => _state.OnHealthChanged -= ChangeHp;
}