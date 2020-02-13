using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public float camPosY = 0.6f;
    public float currentDstFromTarget;
    public float dstFromTarget = 2;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
       // currentDstFromTarget = Mathf.Clamp(currentDstFromTarget, 1.4f, dstFromTarget);

        transform.position = target.position - (transform.forward + new Vector3(0f, camPosY, 0f)) * currentDstFromTarget;

    }
}
