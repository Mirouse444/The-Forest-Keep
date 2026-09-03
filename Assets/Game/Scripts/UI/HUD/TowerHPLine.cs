using UnityEngine;
using UnityEngine.UI;

public class TowerHPLine : MonoBehaviour
{
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private Image _image;

    private void OnEnable() => _healthComponent.State.OnHealthChanged += ChangeHP;
    private void OnDisable() => _healthComponent.State.OnHealthChanged -= ChangeHP;
    
    private void ChangeHP(int currentHp, int maxHP) => _image.fillAmount = (float)currentHp / maxHP;
}
