using UnityEngine;

public class FirstCloud : MonoBehaviour
{    
    [SerializeField] private float _exitXBoundary;

    private float HalfWidth;

    private void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        HalfWidth = spriteRenderer.bounds.extents.x;
    }

    private void Update()
    {
        if (transform.position.x - HalfWidth >= _exitXBoundary)
            Destroy(gameObject);
    }
}
