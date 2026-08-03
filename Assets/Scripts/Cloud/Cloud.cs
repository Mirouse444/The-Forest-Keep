using UnityEngine;
using System;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float _exitXBoundary;

    private SpriteRenderer _spriteRenderer;

    public float HalfWidth { private set; get; }

    public event Action<Cloud> ExitedBounds;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        HalfWidth = _spriteRenderer.bounds.extents.x;
    }

    private void Update()
    {
        if (transform.position.x - HalfWidth >= _exitXBoundary)
        {
            ExitedBounds?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}