using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class UIMagazineReload : MonoBehaviour
{
    [SerializeField] private RectTransform _reloadPanel;
    [SerializeField] private Transform _reloadPosition;
    [SerializeField] private ReloadUIDataSO _reloadUIDataSo;
    [SerializeField] private Image _reloadImage;
    
    private Camera _mainCamera;
    private IReloadUI _magazine;

    private void OnEnable() => _reloadUIDataSo.OnWeaponChanged += InitMagazine;
    private void OnDisable() => _reloadUIDataSo.OnWeaponChanged -= InitMagazine;

    private void InitMagazine()
    {
        RemoveEvent();
        StopReload();
        
        if (_reloadUIDataSo.ReloadUI != null)
            _reloadUIDataSo.ReloadUI.OnReloadStarted += StartReload;

        _magazine = _reloadUIDataSo.ReloadUI;
    }

    private void Awake() => _mainCamera = Camera.main;
    
    private void OnDestroy() => RemoveEvent();

    private void RemoveEvent()
    {
        if (_magazine != null) 
            _magazine.OnReloadStarted -= StartReload;
    }

    
    private void StopReload()
    {
        StopAllCoroutines();
        _reloadPanel.gameObject.SetActive(false);
    }

    private void StartReload(float time)
    {
        StartCoroutine(ReloadCoroutine(time));
        _reloadPanel.gameObject.SetActive(true);
    }

    private IEnumerator ReloadCoroutine(float time)
    {
        float timer = 0f;

        while (time > timer)
        {
            _reloadImage.fillAmount = timer / time;
            timer += Time.deltaTime;
            _reloadPanel.position = _mainCamera.WorldToScreenPoint(_reloadPosition.position);
            yield return null;
        }
        
        _reloadPanel.gameObject.SetActive(false);
    }
}