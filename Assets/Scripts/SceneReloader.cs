using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    public static event Action SceneRestart;

    public void Restart()
    {
        SceneRestart?.Invoke();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
