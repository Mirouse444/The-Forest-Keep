using UnityEngine;

public class MoneyForDeath : MonoBehaviour
{
    [SerializeField] private int _maxCoinForDied;
    [SerializeField] private int _minCoinForDied; 
    
    private ICoinReceiver _coinReceiver;
    private HealthComponent _health;

    private void Awake() => _health = GetComponent<HealthComponent>();
    private void OnDisable() => _health.State.OnDeath -= CoinForDie;
    private void OnEnable() => _health.State.OnDeath += CoinForDie;
    
    public void Initialize(ICoinReceiver receiver) => _coinReceiver = receiver;
    
    private void CoinForDie()
    {
        int dropAmount = Random.Range(_minCoinForDied, _maxCoinForDied);
        _coinReceiver?.AddCoins(dropAmount);
    }
}