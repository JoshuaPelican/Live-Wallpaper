using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private void Update()
    {
        transform.position = WallpaperManager.instance.GetMousePosition();
    }
}
