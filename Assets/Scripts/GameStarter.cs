using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Baloo _baloo;
    [SerializeField] private BackGroundMusic _music;
    [SerializeField] private SceneReloader _reloader;
    [SerializeField] private EnemySpawner _enemySpawner;

    private static bool _wasRestarted;

    private void Awake()
    {
        _startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void Start()
    {
        if (_wasRestarted)
        {
            ActivateGame();
        }
        else
        {
            _baloo.SetRunning(false);
            _startButton.gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        _baloo.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _baloo.GameOver -= OnGameOver;
    }

    private void OnStartButtonClick()
    {
        if (_wasRestarted)
        {
            _reloader.Restart();

            return;
        }

        _wasRestarted = true;
        ActivateGame();
    }

    private void ActivateGame()
    {
        _baloo.SetRunning(true);
        _baloo.GetComponent<BalooMover>()?.MoveUp();
        _startButton.gameObject.SetActive(false);

        _enemySpawner.StartSpawning();
        _music?.PlayMusic();
    }

    private void OnGameOver()
    {
        _enemySpawner.StopSpawning();
        _startButton.gameObject.SetActive(true);
    }
}
