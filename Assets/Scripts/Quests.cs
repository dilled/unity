using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour
{
    public GameObject[] quests;
    public int currentQuest = 0;
    GameObject playerState;
    private void Start()
    {
        playerState = GameObject.Find("PlayerState");
        if (playerState != null)
        {
            currentQuest = playerState.GetComponent<Player>().currentQuest;
            
        }
    }
    public void NextQuest()
    {
        //quests[currentQuest].SetActive(false);
        currentQuest += 1;
       // quests[currentQuest].SetActive(true);
    }
    public GameObject CurrentQuest()
    {
        return  quests[currentQuest];
    }
    
}
