using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackGroundMusic : MonoBehaviour
{
    [SerializeField] Baloo _baloo;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _audioSource.loop = true;
        _audioSource.Stop();
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

    public void PlayMusic()
    {
        if (!_audioSource.isPlaying)
            _audioSource.Play();
    }

    private void OnGameOver()
    {
        _audioSource.Stop();
    }
}
