using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeleeLauncher))]
public class MeleeSwing : MonoBehaviour
{
    [SerializeField] private MeleeCollision _meleeWeaponHitbox;
    [SerializeField] private float _startRotationZ;
    [SerializeField] private float _maxSwingZ;

    [SerializeField] private AnimationCurve _swingCurve;

    private Coroutine _currentAttackRoutine;
    private IMeleeLauncher _launcher;

    private void Awake()
    {
        _launcher = GetComponent<IMeleeLauncher>();
    }

    private void OnEnable() => _launcher.OnFire += StartAttack;

    private void OnDisable() => _launcher.OnFire -= StartAttack;

    private void StartAttack(MeleeStrikeData data)
    {
        if (_currentAttackRoutine == null)
        {
            _meleeWeaponHitbox.Setup(data);
            _currentAttackRoutine = StartCoroutine(AttackRoutine(data));
        }
    }

    private IEnumerator AttackRoutine(MeleeStrikeData data)
    {
        _meleeWeaponHitbox.gameObject.SetActive(true);
        float timer = 0;

        while (data.AttackTime >= timer)
        {
            float progress = timer / data.AttackTime;

            float curveProgress = _swingCurve.Evaluate(progress);

            float currentRotationZ = Mathf.Lerp(_startRotationZ, _maxSwingZ, curveProgress);

            transform.localRotation = Quaternion.Euler(0, 0, currentRotationZ);

            timer += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = Quaternion.Euler(0, 0, _maxSwingZ);
        _meleeWeaponHitbox.gameObject.SetActive(false);
        _currentAttackRoutine = null;
    }
}