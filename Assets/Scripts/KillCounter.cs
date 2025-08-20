using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _killCountText;

    private int _killCount;

    private void Awake()
    {
        ResetCounter();
    }

    private void OnEnable()
    {
        Enemy.AnyEnemyDied += OnAnyEnemyDied;
        SceneReloader.SceneRestart += ResetCounter;
    }

    private void OnDisable()
    {
        Enemy.AnyEnemyDied -= OnAnyEnemyDied;
        SceneReloader.SceneRestart -= ResetCounter;
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
