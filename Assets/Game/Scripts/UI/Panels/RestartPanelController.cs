using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class RestartPanelController : MonoBehaviour
{
    [SerializeField] private PlayerRespawner _respawner;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _text;

    private readonly WaitForSeconds _oneSecond = new WaitForSeconds(1f);
    private Coroutine _countdownCoroutine;

    private void OnEnable() => _respawner.OnRespawnStarted += OpenPanel;
    private void OnDisable() => _respawner.OnRespawnStarted -= OpenPanel;

    private void OpenPanel(int time)
    {
        if (_countdownCoroutine != null)
            StopCoroutine(_countdownCoroutine);

        _countdownCoroutine = StartCoroutine(CountdownCoroutine(time));
    }

    private IEnumerator CountdownCoroutine(int time)
    {
        _panel.gameObject.SetActive(true);

        for (int i = 0; i < time; i++)
        {
            _text.SetText("{0}", time - i);
            
            _text.transform.DOKill();
            _text.transform.localScale = Vector3.one; 
            _text.transform.DOPunchScale(Vector3.one * 0.4f, 0.3f, vibrato: 5, elasticity: 0.5f);

            yield return _oneSecond;
        }

        _text.SetText("");
        _panel.gameObject.SetActive(false);
    }
}