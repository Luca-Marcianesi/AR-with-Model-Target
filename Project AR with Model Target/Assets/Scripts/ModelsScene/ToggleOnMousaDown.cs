using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOnMousaDown : MonoBehaviour
{
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    private void OnMouseDown()
    {
        Trigger();
    }

    public void Trigger()
    {
        if (panel.activeInHierarchy == false)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }
}
