using UnityEngine;
using DG.Tweening;
using System;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class DamagePopup : MonoBehaviour
{
    [SerializeField] private float _animationDuration = 1.2f; 
    private TextMeshPro _text;
    
    private void Awake() => _text = GetComponent<TextMeshPro>();

    public void Setup(int damage, Action<DamagePopup> releaseAction)
    {
        _text.text = damage.ToString();
        _text.alpha = 1f;
        
        Animate(releaseAction);
    }

    private void Animate(Action<DamagePopup> releaseAction)
    {
        float randomDirX = UnityEngine.Random.Range(-1.5f, 1.5f);
        
        Vector3 peakPosition = transform.position + new Vector3(randomDirX * 0.5f, 1.5f, 0); 
        

        Vector3 targetPosition = transform.position + new Vector3(randomDirX, -0.5f, 0);

        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(transform.DOMove(peakPosition, _animationDuration * 0.25f)
            .SetEase(Ease.OutCubic));
        
        sequence.Append(transform.DOMove(targetPosition, _animationDuration * 0.75f)
            .SetEase(Ease.InSine));

        sequence.Insert(_animationDuration * 0.5f, _text.DOFade(0, _animationDuration * 0.5f));
        
        sequence.OnComplete(() => releaseAction.Invoke(this));
    }
}