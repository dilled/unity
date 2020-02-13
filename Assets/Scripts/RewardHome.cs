using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardHome : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Friend")
        {


            other.gameObject.GetComponent<FriendPatrol>().SetOnQuestFalse();
            other.gameObject.GetComponent<FriendController>().onQuest = false;
            other.gameObject.GetComponent<FriendController>().goHome = false;
            other.gameObject.GetComponent<FriendPatrol>().goHome = false;

        }
    }
}