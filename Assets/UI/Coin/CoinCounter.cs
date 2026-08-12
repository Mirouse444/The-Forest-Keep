using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public int CurrentCoin {get; private set; }

    public void addCoin(int amount) => CurrentCoin += amount;
    
    public void SpendingCoin(int amount) => CurrentCoin -= amount;
}