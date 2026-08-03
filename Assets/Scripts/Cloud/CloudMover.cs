using UnityEngine;

public class CloudMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update() => transform.Translate(Vector3.right * Time.deltaTime * _speed);
}
