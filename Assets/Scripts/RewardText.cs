using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardText : MonoBehaviour
{
    bool max = false;
    
    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < 7f && !max)
        {
            transform.localScale += new Vector3(.1f, .1f, 0f);
        }
        else
        {
            max = true;
            transform.localScale += new Vector3(-.1f, -.1f, 0f);
        }
        if (max && transform.localScale.x < 0)
        {
            max = false;
            gameObject.SetActive(false);
        }
    }
    
}
