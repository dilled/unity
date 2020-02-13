using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnable : MonoBehaviour
{
    
    public GameObject jumpLoc;
    void Start()
    {
        jumpLoc.SetActive(true);
    }

    
}
