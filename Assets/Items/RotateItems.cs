using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItems : MonoBehaviour
{
    
    public float yy = 0.5f;

    
    private void Update()
    {
        
        StartCoroutine("RotateItem");
            
    }

    public IEnumerator RotateItem()
    {
        transform.Rotate(0, yy, 0);
        yield return new WaitForSeconds(5f);
    }
}

