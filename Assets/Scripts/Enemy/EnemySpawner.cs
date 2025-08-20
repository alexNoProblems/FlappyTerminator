using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Camera _camera;
    [SerializeField] private Baloo _baloo;
    [SerializeField] private float _spawnInterval = 3f;
    [SerializeField] private float _offscreenMargin = 1f;
    [SerializeField] private float _insideOffset = 0.5f;
    [SerializeField] private float _enterDuration;
    [SerializeField] private float _verticalSpawnIndent = 0.8f;
    [SerializeField] private int _maxEnemies = 4;

    private WaitForSeconds _waitForSeconds;
    private Coroutine _spawnRoutine;
    private int _currentEnemies = 0;
    private bool _isSpawning = false;

    private HashSet<float> _activeHeights = new();

    private void Awake()
    {
        if (_camera == null)
            _camera = Camera.main;

        _waitForSeconds = new WaitForSeconds(_spawnInterval);

        if (_baloo != null)
            _baloo.GameOver += OnGameOver;
    }

    private void OnDestroy()
    {
        _baloo.GameOver -= OnGameOver;
    }

    private void OnDisable()
    {
        StopSpawning();
    }

    public void StartSpawning()
    {
        if (_isSpawning)
            return;

        _isSpawning = true;
        _spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        if (!_isSpawning)
            return;

        _isSpawning = false;

        if (_spawnRoutine != null)
        {
            StopCoroutine(_spawnRoutine);
            _spawnRoutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (_isSpawning)
        {
            SpawnEnemy();

            yield return _waitForSeconds;
        }
    }

    private void OnGameOver()
    {
        StopSpawning();
    }

    private void SpawnEnemy()
    {
        if (_baloo != null && _baloo.IsGameOver)
            return;
        
        if (_currentEnemies >= _maxEnemies)
            return;

        float halfHeight = _camera.orthographicSize;
        float halfWidth = halfHeight * _camera.aspect;

        Vector3 cameraPosition = _camera.transform.position;

        float spawnX = cameraPosition.x + halfWidth + _offscreenMargin;
        float randomY = GetRandomSpawnHeight(cameraPosition, halfHeight);

        if (_activeHeights.Contains(randomY))
            return;

        Vector3 startPosition = new Vector3(spawnX, randomY, 0f);

        Enemy enemy = Instantiate(_enemyPrefab);
        enemy.Init(_camera, _insideOffset);
        enemy.SubscribeToBaloo(_baloo);
        enemy.EnterFromAndStickRight(startPosition, randomY, _enterDuration);

        enemy.Died += () => OnEnemyDied(enemy);

        _currentEnemies++;
        _activeHeights.Add(randomY);
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _currentEnemies = Mathf.Max(0, _currentEnemies - 1);
        _activeHeights.Remove(enemy.SpawnY);
    }

    private float GetRandomSpawnHeight(Vector3 cameraPosition, float halfHeight)
    {
        float yMin = cameraPosition.y - halfHeight + _verticalSpawnIndent;
        float yMax = cameraPosition.y + halfHeight - _verticalSpawnIndent;

        if (yMin > yMax)
            return cameraPosition.y;

        return Random.Range(yMin, yMax);
    }
}
