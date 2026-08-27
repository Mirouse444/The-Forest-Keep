using System;
using UnityEngine;

public class CoinCounter : MonoBehaviour, ICoinReceiver
{
    [SerializeField] private CoinReceiverReferenceSO _coinReceiverReference;
    
    private int _currentCoin;
    
    public event Action<int> OnCoinChange;

    private void Awake() => _coinReceiverReference.Receiver = this;

    public void AddCoins(int amount)
    {
        _currentCoin += amount;
        OnCoinChange?.Invoke(_currentCoin);
    }

    public bool TrySpendingCoins(int amount)
    {
        if(amount > _currentCoin) return false;

        _currentCoin -= amount;
        OnCoinChange?.Invoke(_currentCoin);
        return true;
    }
}