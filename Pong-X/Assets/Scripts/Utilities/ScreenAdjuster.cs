using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]
[ExecuteInEditMode]
public class ScreenAdjuster : MonoBehaviour
{
    public float targetAspect;
    public float originalSize;

    private Vector2 lastScreenSize;
    private Vector2 currentScreenSize;
    private new Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
        lastScreenSize = currentScreenSize = new Vector2(Screen.width, Screen.height);
        originalSize = camera.orthographicSize;
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
    }
}
