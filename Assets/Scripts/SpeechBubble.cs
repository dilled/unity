using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    public Camera cam;
    public int time = 5;
    
    void Update()
    {
        transform.rotation = cam.transform.rotation;
        StartCoroutine("BubbleOff");
    }

    public IEnumerator BubbleOff()
    {
        
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
