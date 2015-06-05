using UnityEngine;

[RequireComponent (typeof(Camera))]
[ExecuteInEditMode]
public class ScreenAdjuster : MonoBehaviour
{
    public float targetAspect;
    public float originalSize;

    Vector2 lastScreenSize;
    Vector2 currentScreenSize;
    new Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
        lastScreenSize = currentScreenSize = new Vector2(Screen.width, Screen.height);
        Adjust();
    }

    void Update()
    {
        currentScreenSize = new Vector2(Screen.width, Screen.height);
        if (lastScreenSize != currentScreenSize)
        {
            lastScreenSize = currentScreenSize;
            Adjust();
        }
    }

    void Adjust()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            camera.orthographicSize = originalSize / scaleHeight;
        }
        if (scaleHeight >= 1.0f)
        {
            camera.orthographicSize = originalSize;
        }
    }
}
