using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeArea : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    RectTransform panelSafeArea;

    //create a rectagle
    Rect currentSafeArea = new Rect();

    //set the orientation
    ScreenOrientation orientation = ScreenOrientation.Portrait;

    // Start is called before the first frame update
    void Start()
    {
        //get the component that controll position and size of the safearea object
        panelSafeArea = GetComponent<RectTransform>();

        orientation = Screen.orientation;

        //get the current safeare
        currentSafeArea = Screen.safeArea;
    }

    void Apply()
    {
        if (panelSafeArea == null) return;


        //create a variable with the current safe area
        Rect safeArea = Screen.safeArea;

        //vector with the posiction of the safearea min
        Vector2 anchorMin = safeArea.position;

        //vector with the posiction of the safearea max
        Vector2 anchorMax = safeArea.position + safeArea.size;

        // normalize the anchor position with the canvas width and height
        anchorMin.x /= canvas.pixelRect.width;
        anchorMin.y /= canvas.pixelRect.height;

        anchorMax.x /= canvas.pixelRect.width;
        anchorMax.y /= canvas.pixelRect.height;

        //set the new value normalized in the safearea object
        panelSafeArea.anchorMin = anchorMin;  //bottom left
        panelSafeArea.anchorMax = anchorMax;  //top right

        //take the current value
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
