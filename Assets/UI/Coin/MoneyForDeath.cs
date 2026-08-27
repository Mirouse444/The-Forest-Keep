using UnityEngine;

public class MoneyForDeath : MonoBehaviour
{
    [SerializeField] private CoinReceiverReferenceSO _receiver;
    [SerializeField] private HealthComponent _health;
    [SerializeField] private int _maxCoinForDied;
    [SerializeField] private int _minCoinForDied; 
    
    private ICoinReceiver _coinReceiver;

    private void Start() => _coinReceiver = _receiver.Receiver;
    private void OnDisable() => _health.State.OnDeath -= CoinForDie;
    private void OnEnable() => _health.State.OnDeath += CoinForDie;
    
    private void CoinForDie()
    {
        int dropAmount = Random.Range(_minCoinForDied, _maxCoinForDied);
        _coinReceiver?.AddCoins(dropAmount);
    }
}