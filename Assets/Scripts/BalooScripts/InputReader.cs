using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const KeyCode TapKey = KeyCode.Mouse0;
    private const KeyCode FireKey = KeyCode.Mouse1;

    public bool TapRequested { get; private set; }
    public bool ShootRequested { get; private set; }
    public bool Enabled { get; set; } = true;

    private void Update()
    {
        if (!Enabled)
            return;

        if (Input.GetKeyDown(TapKey))
            TapRequested = true;

        if (Input.GetKeyDown(FireKey))
            ShootRequested = true;
    }

    public void CleanTapRequested()
    {
        TapRequested = false;
    }
    
    public void CleanShootRequested()
    {
        ShootRequested = false;
    }
}
