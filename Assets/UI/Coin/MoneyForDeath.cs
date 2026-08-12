using UnityEngine;

public class MoneyForDeath : MonoBehaviour
{
    [SerializeField] private int _maxCoinForDied;
    [SerializeField] private int _minCoinForDied; 
    [SerializeField] private  CoinCounter _coinCounter;

    private HealthComponent _moneyTrigger;

    private void Awake() => _moneyTrigger = GetComponent<HealthComponent>();

    private void OnEnable() => _moneyTrigger.State.OnDeath += CoinForDie;
    
    private void OnDisable() => _moneyTrigger.State.OnDeath -= CoinForDie;

    private void CoinForDie() => _coinCounter.addCoin(Random.Range(_minCoinForDied, _maxCoinForDied));
}