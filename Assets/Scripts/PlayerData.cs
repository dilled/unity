using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float maxHealth;
    public float maxStamina;
    public float maxDive;
    public bool ableSwim;
    public bool ableFlosh;
    public int diamonds;

    public float[] position;
    //public Vector3 startpos;
    public int currentQuest;
    public float[,] diamondPosition;
    public int diamondsLeft;
    //public List<Transform> diamondsList;
    

    public PlayerData(Player player)
    {
        maxHealth = player.maxHealth;
        maxStamina = player.maxStamina;
        maxDive = player.maxDive;
        ableSwim = player.ableSwim;
        ableFlosh = player.ableFlosh;
        diamonds = player.diamonds;

        position = new float[3];
        position[0] = player.position[0];
        position[1] = player.position[1];
        position[2] = player.position[2];
        //startpos = player.transform.position;
        currentQuest = player.currentQuest;
        diamondsLeft = player.diamondsLeft;
        diamondPosition = new float[player.diamondsLeft,3];
        for (int i = 0; i < player.diamondsLeft; i++)
        {
            //Debug.Log(diamondsList[i].position.x + i);

            diamondPosition[i, 0] = player.diamondPosition[i,0];
            diamondPosition[i, 1] = player.diamondPosition[i,1];
            diamondPosition[i, 2] = player.diamondPosition[i,2];
        }
            //diamondPosition[0,0] = player.diamondPosition[0,0];
       // diamondsList = player.diamondsList;
    }
    
}
