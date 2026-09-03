using UnityEngine;

public class OpenShop : MonoBehaviour
{
    [SerializeField] private GameObject _shop;
    [SerializeField] private DayTrigger _trigger;

    private void OnEnable() => _trigger.OnDayStarted += Open;
    private void OnDisable() => _trigger.OnDayStarted -= Open;

    private void Open() => _shop.SetActive(true);
}