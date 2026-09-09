using UnityEngine;

[CreateAssetMenu(menuName = "Pool/MobeScriptable/Coin Receiver Reference", fileName = "Coin Receiver Reference", order = 0)]
public class CoinReceiverReferenceSO : ScriptableObject
{
    public ICoinReceiver Receiver {get; set; }
}