using System;
using UnityEngine;
using UnityEngine.UI;

public class GameStarter : SceneHandler
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Baloo _baloo;
    [SerializeField] private BackGroundMusic _music;
    [SerializeField] private EnemySpawner _enemySpawner;

    private bool _isStarted;
    
    public event Action PlayButtonClicked;
    
    private void Awake()
    {
        _startButton.onClick.AddListener(OnStartButtonClick);
        _restartButton.onClick.AddListener(OnRestartButtonClick);
    }
                            
    private void Start()
    {
        if (ConsumeAutoStartFlag())
        {
            ActivateGame();
            _startButton.gameObject.SetActive(false);
            _restartButton.gameObject.SetActive(false);
        }
        else
        {
            _baloo.SetRunning(false);
            _startButton.gameObject.SetActive(true);
            _restartButton.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (_baloo != null)
            _baloo.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        if (_baloo != null)
            _baloo.GameOver -= OnGameOver;
    }

    protected override void OnButtonClick()
    {
        OnStartButtonClick();
    }

    private void OnStartButtonClick()
    {
        ActivateGame();
        _startButton.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);
        PlayButtonClicked?.Invoke();
    }

    private void OnRestartButtonClick()
    {
        Restart();
    }

    private void ActivateGame()
    {
        if (_isStarted)
            return;

        _baloo.SetRunning(true);
        _baloo.GetComponent<BalooMover>()?.MoveUp();
        _enemySpawner.StartSpawning();
        _music?.PlayMusic();
    }

    private void OnGameOver()
    {
        _enemySpawner?.StopSpawning();
        _restartButton.gameObject.SetActive(true);
    }
}
