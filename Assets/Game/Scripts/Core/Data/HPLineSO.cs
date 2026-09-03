using UnityEngine;

[CreateAssetMenu(menuName = "Settings/MobeScriptable/HP Line", fileName = "HP Line", order = 0)]
public class HPLineSO : ScriptableObject
{
    public IHPLineSpawner HPLineSpawner {get; set; }
}