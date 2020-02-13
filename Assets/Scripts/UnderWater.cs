using UnityEngine;
using System.Collections;

public class UnderWater : MonoBehaviour
{
    public float waterLevel;
   // public Transform waterPlane;    //  Testing
    public bool isUnderwater;
    private Color normalColor;
    private Color underwaterColor;

    CharacterStats myStats;
    // Use this for initialization
    void Start()
    {
        normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        underwaterColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
        myStats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isUnderwater)
        {
            myStats.Diving();
        }
        else
        {
            myStats.GainDive();
        }
        if ((transform.position.y < waterLevel) != isUnderwater)
        {
            if (gameObject.GetComponent<PlayerController2>().swimming) {
                isUnderwater = transform.position.y < waterLevel;
                if (isUnderwater) SetUnderwater();
                if (!isUnderwater) SetNormal();
            }
        }
    }

    void SetNormal()
    {
        RenderSettings.fogColor = normalColor;
        RenderSettings.fogDensity = 0.01f;
        RenderSettings.fog = true;

        //  Testing
        // waterPlane.localScale = new Vector3(waterPlane.localScale.x, 1.0f, waterPlane.localScale.z);
    }

    void SetUnderwater()
    {
        RenderSettings.fogColor = underwaterColor;
        RenderSettings.fogDensity = 0.1f;
        RenderSettings.fog = true;
        
        //  Testing
       // waterPlane.localScale = new Vector3(waterPlane.localScale.x, -1.0f, waterPlane.localScale.z);
    }
}
