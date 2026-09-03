using UnityEngine;

public class SpriteFader : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _spriteRenderers;
    [SerializeField] private float _fadeTime;
    [SerializeField] private AnimationCurve _fadeCurve;

    private float _currentFadeTime;

    private void OnEnable() => _currentFadeTime = 0f;

    private void Update()
    {
        if(_currentFadeTime >= _fadeTime) return;

        float alpha = _fadeCurve.Evaluate(_currentFadeTime / _fadeTime);
        
        foreach (var spriteRenderer in _spriteRenderers)
        {
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }

        _currentFadeTime += Time.deltaTime;
    }
}