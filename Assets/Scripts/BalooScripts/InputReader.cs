using UnityEngine;
using UnityEngine.EventSystems;

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

        bool isOverUI = IsPointerOverUI();

        if (!isOverUI && Input.GetKeyDown(TapKey))
            TapRequested = true;

        if (!isOverUI && Input.GetKeyDown(FireKey))
            ShootRequested = true;
    }

    public void RequestTap()
    {
        if (Enabled)
            TapRequested = true;
    }

    public void RequestShoot()
    {
        if (Enabled)
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

    private bool IsPointerOverUI()
    {
        if (EventSystem.current == null)
            return false;

        if (Input.touchCount > 0)
            return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);

        return EventSystem.current.IsPointerOverGameObject();
    }
}
