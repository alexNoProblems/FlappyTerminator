using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _killCountText;
    [SerializeField] private EnemySpawner _enemySpawner;

    private int _killCount;

    private void Awake()
    {
        ResetCounter();
    }

    private void OnEnable()
    {
        _enemySpawner.EnemyDied += OnAnyEnemyDied;
    }

    private void OnDisable()
    {
        _enemySpawner.EnemyDied -= OnAnyEnemyDied;
    }

    private void OnAnyEnemyDied(Enemy enemy)
    {
        _killCount++;
        UpdateUI();
    }

    private void ResetCounter()
    {
        _killCount = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _killCountText?.SetText(_killCount.ToString());
    }
}
