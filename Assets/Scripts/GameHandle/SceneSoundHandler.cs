using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneSoundHandler : MonoBehaviour
{
    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;
    [SerializeField] private Image _iconImage;

    private Button _button;
    private bool _isMuted;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ToggleSound);

        _isMuted = AudioListener.pause;
        UpdateIcon();
    }

    private void ToggleSound()
    {
        _isMuted = !_isMuted;
        AudioListener.pause = _isMuted;

        UpdateIcon();
    }

    private void UpdateIcon()
    {
        if (_iconImage != null)
        {
            _iconImage.sprite = _isMuted ? _soundOffSprite : _soundOnSprite;
        }
    }

}
