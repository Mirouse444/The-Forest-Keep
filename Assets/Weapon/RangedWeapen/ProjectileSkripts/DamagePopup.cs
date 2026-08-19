using UnityEngine;
using DG.Tweening;
using System;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class DamagePopup : MonoBehaviour
{
    // Немного увеличим базовое время, можешь крутить его в Инспекторе
    [SerializeField] private float _animationDuration = 1.2f; 
    private TextMeshPro _text;
    
    private void Awake() => _text = GetComponent<TextMeshPro>();

    public void Setup(int damage, Action<DamagePopup> releaseAction)
    {
        _text.text = damage.ToString();
        _text.alpha = 1f; // Сбрасываем прозрачность
        
        Animate(releaseAction);
    }

    private void Animate(Action<DamagePopup> releaseAction)
    {
        // 1. Рассчитываем точки
        float randomDirX = UnityEngine.Random.Range(-1.5f, 1.5f);
        
        // Высшая точка (куда текст резко подскочит)
        Vector3 peakPosition = transform.position + new Vector3(randomDirX * 0.5f, 1.5f, 0); 
        
        // Конечная точка (куда будет плавно опускаться)
        Vector3 targetPosition = transform.position + new Vector3(randomDirX, -0.5f, 0);

        Sequence sequence = DOTween.Sequence();
        
        // 2. Быстрый подскок вверх (занимает всего 25% от всего времени)
        // Ease.OutCubic делает так, что в начале скорость максимальная, а к верхней точке текст замедляется
        sequence.Append(transform.DOMove(peakPosition, _animationDuration * 0.25f)
            .SetEase(Ease.OutCubic));
        
        // 3. Медленное падение вниз (занимает оставшиеся 75% времени)
        // Ease.InSine заставляет текст падать мягко, плавно ускоряясь вниз
        sequence.Append(transform.DOMove(targetPosition, _animationDuration * 0.75f)
            .SetEase(Ease.InSine));
        
        // 4. Текст начинает растворяться ровно с середины падения
        sequence.Insert(_animationDuration * 0.5f, _text.DOFade(0, _animationDuration * 0.5f));
        
        sequence.OnComplete(() => releaseAction.Invoke(this));
    }
}