using UnityEngine;
using UnityEngine.UI;

public class GameOverFilter : MonoBehaviour
{
    [SerializeField] private Baloo _baloo;
    [SerializeField] private Image _gameOverFilter;
    [SerializeField] private Image _gameOverTextImage;

    private void Awake()
    {
        if (_gameOverFilter != null)
            _gameOverFilter.enabled = false;
        
        if (_gameOverTextImage != null)
            _gameOverTextImage.enabled = false;
    }

    private void OnEnable()
    {
        _baloo.GameOver += ShowFilter;
    }

    private void OnDisable()
    {
        _baloo.GameOver -= ShowFilter;
    }

    private void ShowFilter()
    {
        if (_gameOverFilter != null)
            _gameOverFilter.enabled = true;
        
        if (_gameOverTextImage != null)
            _gameOverTextImage.enabled = true; 
    }
}
