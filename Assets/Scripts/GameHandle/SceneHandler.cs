using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SceneHandler : MonoBehaviour
{
    [SerializeField] private RestartToken _restartToken;

    public event Action Restarting;
    
    protected bool ConsumeAutoStartFlag()
    {
        if (_restartToken != null && _restartToken.AutoStart)
        {
            _restartToken.AutoStart = false;

            return true;
        }

        return false;
    }

    protected void Restart()
    {
        Restarting?.Invoke();

        if (_restartToken != null)
            _restartToken.AutoStart = true;

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    protected abstract void OnButtonClick();  
}
