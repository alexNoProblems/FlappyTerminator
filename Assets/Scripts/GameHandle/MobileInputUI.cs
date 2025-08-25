using UnityEngine;
using UnityEngine.UI;

public class MobileInputUI : MonoBehaviour
{
    [SerializeField] private Button _upButton;
    [SerializeField] private Button _fireButton;
    [SerializeField] private InputReader _inputReader;

    private void Awake()
    {
        if (_upButton != null)
            _upButton.onClick.AddListener(OnUpClicked);

        if (_fireButton != null)
            _fireButton.onClick.AddListener(OnFireClicked);
    }

    private void OnDestroy()
    {
        if (_upButton != null)
            _upButton.onClick.RemoveListener(OnUpClicked);

        if (_fireButton != null)
            _fireButton.onClick.RemoveListener(OnFireClicked);
    }

    private void OnUpClicked()
    {
        _inputReader?.RequestTap();
    }

    private void OnFireClicked()
    {
        _inputReader?.RequestShoot();
    }
}
