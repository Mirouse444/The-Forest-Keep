using UnityEngine;
using System.Collections.Generic;

public class CloudPool : MonoBehaviour
{
    [SerializeField] private Cloud[] _allSceneClouds;
    
    private List<Cloud> _freeClouds;

    private void Awake() => _freeClouds = new List<Cloud>(_allSceneClouds);

    private void Start()
    {
        foreach (Cloud cloud in _allSceneClouds)
            cloud.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        foreach (Cloud cloud in _allSceneClouds)
            cloud.ExitedBounds += AddCloud;
    }

    private void OnDisable()
    {
        foreach (Cloud cloud in _allSceneClouds)
            cloud.ExitedBounds -= AddCloud;
    }

    private void AddCloud(Cloud cloud) => _freeClouds.Add(cloud);

    public Cloud GetRandomCloud()
    {
        if (_freeClouds.Count == 0)
            return null;

        int randomIndex = Random.Range(0, _freeClouds.Count);
        Cloud randomCloud = _freeClouds[randomIndex];
        _freeClouds.RemoveAt(randomIndex);
        
        return randomCloud;
    }
}
