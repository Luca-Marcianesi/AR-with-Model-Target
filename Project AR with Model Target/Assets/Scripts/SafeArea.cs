using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeArea : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    RectTransform panelSafeArea;

    Rect currentSafeArea = new Rect();
    ScreenOrientation orientation = ScreenOrientation.Portrait;

    // Start is called before the first frame update
    void Start()
    {
        panelSafeArea = GetComponent<RectTransform>();

        orientation = Screen.orientation;
        currentSafeArea = Screen.safeArea;
    }

    void Apply()
    {
        if (panelSafeArea == null) return;

        Rect safeArea = Screen.safeArea;
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position - safeArea.size;

        anchorMin.x /= canvas.pixelRect.width;
        anchorMin.y /= canvas.pixelRect.height;

        anchorMax.x /= canvas.pixelRect.width;
        anchorMax.y /= canvas.pixelRect.height;

        panelSafeArea.anchorMin = anchorMin;
        panelSafeArea.anchorMax = anchorMax;

        orientation = Screen.orientation;
        currentSafeArea = Screen.safeArea;

    }

    // Update is called once per frame
    void Update()
    {
        if (orientation != Screen.orientation || (currentSafeArea != Screen.safeArea))
        {
            Apply();
        }

    }
}
