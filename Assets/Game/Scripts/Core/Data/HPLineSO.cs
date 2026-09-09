using UnityEngine;

[CreateAssetMenu(menuName = "Pool/MobeScriptable/HP Line", fileName = "HP Line", order = 0)]
public class HPLineSO : ScriptableObject
{
    public IHPLineSpawner HPLineSpawner {get; set; }
}