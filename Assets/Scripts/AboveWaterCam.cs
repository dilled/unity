using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboveWaterCam : MonoBehaviour
{
    private Color normalColor;
    // Start is called before the first frame update
    void Update()
    {
        normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        RenderSettings.fogColor = normalColor;
        RenderSettings.fogDensity = 0.01f;
        RenderSettings.fog = true;
    }

}
