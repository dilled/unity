using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSets
{
   
    public float timePassed;
    public float timePassedWait;
    public bool clicked;
    public float sensitivity;

    public PlayerSets(Player player)
    {
        timePassed = player.timePassed;
        timePassedWait = player.timePassedWait;
        clicked = player.clicked;
        sensitivity = player.sensitivity;
    }
}
