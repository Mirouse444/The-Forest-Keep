using UnityEngine;

public class SpriteFader : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _fadeTime;
    [SerializeField] AnimationCurve _fadeCurve;

    private float _currentFadeTime;

    private void OnEnable() => _currentFadeTime = 0f;

    private void Update()
    {
        if(_currentFadeTime < _fadeTime)
        {
            Color color = _spriteRenderer.color;
            color.a = _fadeCurve.Evaluate(_currentFadeTime / _fadeTime);
            _spriteRenderer.color = color;

            _currentFadeTime += Time.deltaTime;
        }
    }
}