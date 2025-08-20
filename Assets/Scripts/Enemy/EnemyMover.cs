using System;
using System.Collections;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private const float FullProgress = 1f;
    private const float MinDuration = 0.0001f;
    private const float DefaultEnterDuration = 0.5f;

    [SerializeField] private float _easingPower = 3f;

    private Coroutine _routine;

    public void StartEnter(Vector3 from, Func<Vector3> targetProvider, float duration, Action onComplete)
    {
        if (_routine != null)
            StopCoroutine(_routine);

        if (duration <= 0f)
            duration = DefaultEnterDuration;

        _routine = StartCoroutine(EnterRoutine(from, targetProvider, duration, onComplete));
    }

    private IEnumerator EnterRoutine(Vector3 from, Func<Vector3> targetProvider, float duration, Action onComplete)
    {
        float progress = 0f;

        while (progress < FullProgress)
        {
            progress += Time.deltaTime / Mathf.Max(MinDuration, duration);
            Vector3 to = targetProvider?.Invoke() ?? transform.position;

            float eased = FullProgress - Mathf.Pow(FullProgress - progress, _easingPower);
            transform.position = Vector3.LerpUnclamped(from, to, eased);

            yield return null;
        }

        _routine = null;
        onComplete?.Invoke();
    }
}
