using System;
using UnityEngine;

public class EnemyHover : MonoBehaviour
{
    [SerializeField] private float _hoverAmplitude = 0.15f;
    [SerializeField] private float _hoverFrequency = 1.2f;

    private bool _isActive;
    private Func<Vector3> _basePositionProvider;

    private void Update()
    {
        if (!_isActive || _basePositionProvider == null)
            return;

        Vector3 basePosition = _basePositionProvider.Invoke();
        float yPosition = Mathf.Sin(Time.time * _hoverFrequency) * _hoverAmplitude;
        transform.position = basePosition + new Vector3(0f, yPosition, 0f); 
    }

    public void StartHover(Func<Vector3> basePositionProvider)
    {
        _basePositionProvider = basePositionProvider;
        _isActive = true;
    }

    public void StopHover()
    {
        _isActive = false;
        _basePositionProvider = null;
    }
}
