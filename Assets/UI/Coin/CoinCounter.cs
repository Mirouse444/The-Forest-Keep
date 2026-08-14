using System;
using UnityEngine;

public class CoinCounter : MonoBehaviour, ICoinReceiver
{
    private int _currentCoin;
    public event Action<int> OnCoinChange;
    
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