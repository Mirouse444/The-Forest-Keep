using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    private const int ApplesCount = 5;
    
    [SerializeField] private HealthComponent _playerHealth;

    [SerializeField] private Image[] _greenApples =  new Image[ApplesCount];
    
    [SerializeField] private int _oneAppleHP = 20; 

    private void OnEnable()
    {
        if (_playerHealth)
            _playerHealth.State.OnHealthChanged += UpdateHpStatus;
    }

    private void OnDisable()
    {
        if (_playerHealth)
            _playerHealth.State.OnHealthChanged -= UpdateHpStatus;
    }

    private void UpdateHpStatus(int currentHP, int maxHP)
    {
        float remainingHP = currentHP;

        for (int i = 0; i < ApplesCount; i++)
        {
            float appleHP = Mathf.Clamp(remainingHP, 0, _oneAppleHP);

            _greenApples[i].fillAmount = appleHP / _oneAppleHP;

            remainingHP -= _oneAppleHP;
        }
    }
}
