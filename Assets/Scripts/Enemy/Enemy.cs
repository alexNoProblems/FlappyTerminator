using System;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(EnemyMover), typeof(EnemyHover), typeof(EnemyShooter))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _insideOffset = 0.5f;

    private EnemyMover _mover;
    private EnemyHover _hover;
    private EnemyShooter _shooter;
    private Baloo _baloo;
    private float _targetYPosition;
    private float _spawnY;
    private bool _isStopShooting;

    public float SpawnY { get; private set; }

    public event Action Died;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
        _hover = GetComponent<EnemyHover>();
        _shooter = GetComponent<EnemyShooter>();
    }

    public void Init(Camera camera, float insideOffset)
    {
        _camera = camera;
        _insideOffset = insideOffset;
    }

    private void OnDisable()
    {
        if (_baloo != null)
            _baloo.GameOver -= OnGameOver;
    }

    public void SubscribeToBaloo(Baloo baloo)
    {
        if (_baloo != null)
            _baloo.GameOver -= OnGameOver;

        _baloo = baloo;

        if (_baloo != null)
            _baloo.GameOver += OnGameOver;
    }

    public void SetSpawnY(float yPosition)
    {
        _spawnY = yPosition;
    }

    public void EnterFromRight(Vector3 spawnPosition, float targetY, float duration)
    {
        _targetYPosition = targetY;
        transform.position = spawnPosition;
    }

    public void EnterFromAndStickRight(Vector3 spawnPosition, float targetY, float duration)
    {
        _targetYPosition = targetY;
        transform.position = spawnPosition;
        float targetYLocal = _targetYPosition;

        Vector3 TargetProvider()
        {
            float halfWidth = _camera.orthographicSize * _camera.aspect;
            float rightEdgeX = _camera.transform.position.x + halfWidth - _insideOffset;

            return new Vector3(rightEdgeX, targetYLocal, 0f);
        }

        _hover.StopHover();
        _mover.StartEnter(
            from: spawnPosition,
            targetProvider: TargetProvider,
            duration: duration,
            onComplete: () =>
            {
                _hover.StartHover(TargetProvider);
                if (!_isStopShooting && (_baloo == null || !_baloo.IsGameOver))
                    _shooter?.StartShooting();
            });
    }

    public void Kill()
    {
        _hover?.StopHover();
        _shooter?.StopShooting();

        Died?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnGameOver()
    {
        _isStopShooting = true;
        _shooter?.StopShooting();
        _hover?.StopHover();
    }
}
