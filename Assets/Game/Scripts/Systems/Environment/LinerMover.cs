using UnityEngine;

public class LinerMover : MonoBehaviour
{
    [SerializeField, Min(0)] private float _speed;

    private void Update() => transform.Translate(Vector3.right * (Time.deltaTime * _speed));
}
