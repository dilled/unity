using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Timeline : MonoBehaviour
{
    public PlayableDirector timeline;
    // Start is called before the first frame update
    void Start()
    {
       // Debug.Log("timeline start");
        timeline = GetComponent<PlayableDirector>();
        timeline.Play();
    }

   
}
