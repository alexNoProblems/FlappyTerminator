using System;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(BalooMover), typeof(InputReader), typeof(Rigidbody2D))]
[RequireComponent(typeof(BalooShooter))]
public class Baloo : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioClip _shootSfx;

    private BalooMover _mover;
    private InputReader _inputReader;
    private Rigidbody2D _rigidbody2D;
    private BalooShooter _shooter;
    private bool _isGameOver;
    private bool _running;

    public bool IsGameOver => _isGameOver;

    public event Action GameOver;

    private void Awake()
    {
        _mover = GetComponent<BalooMover>();
        _shooter = GetComponent<BalooShooter>();
        _inputReader = GetComponent<InputReader>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        if (_shooter != null)
            _shooter.ShootFired += PlayShootSfx;

        SetRunning(false);
    }

    private void Update()
    {
        if (_isGameOver || !_running)
            return;

        if (_inputReader.TapRequested)
        {
            _mover.MoveUp();
            _inputReader.CleanTapRequested();
        }

        if (_inputReader.ShootRequested)
        {
            _shooter?.TryShoot();
            _inputReader.CleanShootRequested();
        }

        _mover.RotateDown();
    }

    private void OnDisable()
    {
        if (_shooter != null)
            _shooter.ShootFired -= PlayShootSfx;
    }

    public void SetRunning(bool value)
    {
        _running = value;
        _inputReader.enabled = value;
        _mover.enabled = value;
        _shooter.enabled = value;

        if (_rigidbody2D)
        {
            _rigidbody2D.simulated = value;

            if (!value)
            {
                _rigidbody2D.velocity = Vector2.zero;
                _rigidbody2D.angularVelocity = 0f;
            }
        }

        if (_animator != null)
            _animator.speed = value ? 1f : 0f;
    }

    public void TriggerGameOver()
    {
        if (_isGameOver)
            return;

        _isGameOver = true;
        SetRunning(false);
        GameOver?.Invoke();
    }

    private void PlayShootSfx()
    {
        if (_sfxSource != null && _shootSfx != null)
            _sfxSource.PlayOneShot(_shootSfx);
    }
}
