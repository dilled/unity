using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWaterCam : MonoBehaviour
{
    private Color underwaterColor;
    // Start is called before the first frame update
    void Update()
    {
        underwaterColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
        RenderSettings.fogColor = underwaterColor;
        RenderSettings.fogDensity = 0.1f;
        RenderSettings.fog = true;
    }
    
}
