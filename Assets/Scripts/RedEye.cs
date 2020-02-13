using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedEye : MonoBehaviour
{
    public Image hidingEye;
    //public Image eye;

    private void Start()
    {
        hidingEye = GameObject.Find("HidingEye").GetComponent<Image>();
       // eye = GameObject.Find("Eye").GetComponent<Image>();
    }
    public void AttackEye()
    {
        if (hidingEye != null)
        { 
            hidingEye.fillAmount = 1;// attackTime / maxTime;
        }
    }
    public void NotAttackEye()
    {
        if (hidingEye != null)
        {
            hidingEye.fillAmount = 0;// attackTime / maxTime;
        }
    }
    
}
