using UnityEngine;
using UnityEngine.UI;

public class HPLine : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private GameObject _visualContainer;
    [SerializeField] private float _timeToOff;

    private float _timer = 0f;
    private IHealthState _state;

    public RectTransform HPLineRectTransform { get; private set; }

    private void Awake()
    {
        HPLineRectTransform = GetComponent<RectTransform>();
    }

    public void ChangeState(IHealthState state)
    {
        if (_state != null)
            _state.OnHealthChanged -= ChangeHp;

        _state = state;
        _state.OnHealthChanged += ChangeHp;

        if (_visualContainer != null)
            _visualContainer.SetActive(false);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_timeToOff <= _timer)
        {
            _visualContainer.SetActive(false);
        }
    }

    private void ChangeHp(int currentHealth, int maxHealth)
    {
        if (currentHealth <= 0)
        {
            _state.OnHealthChanged -= ChangeHp;

            if (_visualContainer != null)
                _visualContainer.SetActive(false);
        }
        else
        {
            if (_visualContainer != null)
                _visualContainer.SetActive(true);

            _timer = 0f;
            _fillImage.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    private void OnDisable()
    {
        if (_state != null)
        {
            _state.OnHealthChanged -= ChangeHp;
            _state = null;
        }
    }
}