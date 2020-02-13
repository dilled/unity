using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimManager : MonoBehaviour
{ 

    public delegate void OnPlayerSwimming();
    public static OnPlayerSwimming onPlayerSwimming;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {/*
            if (falling >= 3f)
            {
                sounds.SplashAudio();
            }
            swimming = true;
            //Debug.Log(other.transform.name);
            */
            onPlayerSwimming?.Invoke();
        }
    }
}
