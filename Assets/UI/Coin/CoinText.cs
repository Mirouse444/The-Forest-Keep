using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    [SerializeField] private CoinCounter _counter;
    
    private TextMeshProUGUI _coinCountText;

    private void OnEnable() => _counter.OnCoinChange += SetCoin;
    private void OnDisable() => _counter.OnCoinChange -= SetCoin;
    
    private void Awake() => _coinCountText = GetComponent<TextMeshProUGUI>();
    
    private void SetCoin(int  coin) => _coinCountText.SetText("{0}", coin);
}