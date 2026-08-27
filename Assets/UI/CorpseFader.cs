using UnityEngine;

public class CorpseFader : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 2f;
    [SerializeField] private AnimationCurve _fadeCurve;
    
    private static readonly int ColorProperty = Shader.PropertyToID("_Color");

    private SkinnedMeshRenderer[] _renderers;
    private MaterialPropertyBlock _propBlock;
    private float _timer;
    private bool _isFinished;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        _propBlock = new MaterialPropertyBlock();
    }

    private void Update()
    {
        if (_isFinished) return;

        if (_timer < _fadeDuration)
        {
            _timer += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(_timer / _fadeDuration);

            float alpha = _fadeCurve.Evaluate(normalizedTime);
            Color currentColor = new Color(1f, 1f, 1f, alpha);

            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].GetPropertyBlock(_propBlock);
                _propBlock.SetColor(ColorProperty, currentColor);
                _renderers[i].SetPropertyBlock(_propBlock);
            }
        }
        else
            _isFinished = true;
    }
}