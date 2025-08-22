using UnityEngine;

[CreateAssetMenu(fileName = "RestartToken", menuName = "Game/Restart Token")]
public class RestartToken : ScriptableObject
{
    [SerializeField] private bool _autoStart;

    public bool AutoStart
    {
        get => _autoStart;
        set => _autoStart = value;
    }
}
