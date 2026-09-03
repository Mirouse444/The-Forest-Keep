using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class UIMagazineReload : MonoBehaviour
{
    [SerializeField] private RectTransform _reloadPanel;
    [SerializeField] private Image _reloadImage;
    [SerializeField] private Transform _reloadPosition;

    private Camera _mainCamera;
    private IReloadUI _magazine;

    public void InitMagazine(IReloadUI magazine)
    {
        RemoveEvent();
        StopReload();
        
        if (magazine != null)
            magazine.OnReloadStarted += StartReload;

        _magazine = magazine;
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