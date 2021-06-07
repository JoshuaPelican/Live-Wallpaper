using UnityEngine;

public class WallpaperManager : MonoBehaviour
{
    #region Startup Actions
    public static WallpaperManager instance;

    private void Awake()
    {
        instance = this;

        SystemTray tray = Rainity.CreateSystemTrayIcon();
        tray.AddItem("Exit Wallpaper", ExitWallpaper);
    }
    #endregion

    bool useClickEffects = true;
    Camera mainCam;
    public GameObject clickSystem;

    public void AddToStartup()
    {
        Rainity.AddToStartup();
    }
    public void RemoveFromStartup()
    {
        Rainity.RemoveFromStartup();
    }
    public void ExitWallpaper()
    {
        Application.Quit();
    }
    public Vector2 GetMousePosition()
    {
        return mainCam.ScreenToWorldPoint(RainityInput.mousePosition);
    }

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        CheckClickEffects();
    }

    private void CheckClickEffects()
    {
        if (useClickEffects)
        {
            if (RainityInput.GetMouseButtonDown(0))
            {
                Instantiate(clickSystem, GetMousePosition(), Quaternion.identity).GetComponent<ParticleSystem>();
            }
        }
    }
}
