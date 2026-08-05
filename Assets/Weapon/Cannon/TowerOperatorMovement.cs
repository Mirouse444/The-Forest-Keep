using UnityEngine;

public class TowerOperatorMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4f;
    
    private Vector3 _rightScale;
    private Vector3 _leftScale;

    private void Awake()
    {
        _rightScale = transform.localScale;
        _leftScale =  new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public bool MoveTo(Vector3 targetPosition)
    {
        Vector3 targetPosWithSameY = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosWithSameY, _moveSpeed * Time.deltaTime);

        if (transform.position.x > targetPosition.x)
            transform.localScale = _leftScale;
        else if (transform.position.x < targetPosition.x)
            transform.localScale = _rightScale;

        return Mathf.Abs(transform.position.x - targetPosition.x) < 0.1f;
    }
}