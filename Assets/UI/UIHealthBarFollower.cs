using UnityEngine;
using UnityEngine.UI;

public class UIHealthBarFollower : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    private Transform _followPoint;

    public void Init(Transform followPoint)
    {
        _followPoint = followPoint;
    }

    private void LateUpdate()
    {
        if (_followPoint != null)
        {
            transform.position = _followPoint.position;
            transform.rotation = Quaternion.identity;
        }
    }

    public void UpdateFill(float fillAmount)
    {
        _fillImage.fillAmount = fillAmount;
    }
}