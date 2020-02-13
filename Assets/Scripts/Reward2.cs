using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reward2 : MonoBehaviour
{
    
    public string rewardText;
    public Text rewardtext;
    public GameObject reward;
    public bool friendTouched = false;
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Friend")
        {
            if (reward != null)
            {
                reward.gameObject.SetActive(true);
                //other.gameObject.GetComponent<FriendPatrol>().SetOnQuestFalse();
                other.gameObject.GetComponent<FriendController>().goHome = true;
                friendTouched = true;
                other.gameObject.GetComponent<FriendPatrol>().GoHome();
            }
        }
        if (other.transform.tag == "Player")
        {

            //PlayerManager.instance.startPos = gameObject.transform;
            if (reward != null)
            {
                if (friendTouched)
                {
                    rewardtext.gameObject.SetActive(true);
                    rewardtext.text = rewardText.ToString();
                }
            }
           // PlayerManager.instance.GetComponent<Player>().SavePlayer();
            //reward.gameObject.SetActive(false);
            //GetComponent<BoxCollider>().gameObject.SetActive(false);

        }
    }
        
}
