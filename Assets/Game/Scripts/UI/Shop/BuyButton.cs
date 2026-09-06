using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BuyButton : MonoBehaviour
{
    [SerializeField] private CoinCounter _coinCounter; 
    [SerializeField] private int _price;
    
    private Button _buyButton;

    public event System.Action OnBuy;
    
    private void Awake() => _buyButton = GetComponent<Button>();

    private void OnEnable() => _buyButton.onClick.AddListener(TryToBuy);

    private void OnDisable() => _buyButton.onClick.RemoveListener(TryToBuy);
    
    private void TryToBuy()
    {
        if (_coinCounter.TrySpendingCoins(_price))
        {
            OnBuy?.Invoke();
            Destroy(gameObject);
        }
    }
}