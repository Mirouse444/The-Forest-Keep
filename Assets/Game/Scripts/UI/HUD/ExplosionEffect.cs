using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private float _timeToDespawn;
    
    private float _timer;
    public System.Action<ExplosionEffect> Release { private get; set; }

    private void OnEnable() => _timer = 0;
    
    public void Play(float radius, Vector3 position)
    {
        transform.position = position;
        transform.localScale = new Vector3(radius, radius, radius); 
        
        _particles.Play();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        
        if(_timer >= _timeToDespawn)
            Release.Invoke(this);
    }
}