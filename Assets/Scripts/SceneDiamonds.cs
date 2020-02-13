using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDiamonds : MonoBehaviour
{
    public GameObject parent;
    public Transform spots;
    public List<Transform> diamonds;
    public GameObject diamond;
    GameObject playerState;

    // Start is called before the first frame update
    void Start()
    {
        playerState = GameObject.Find("PlayerState");
        if (playerState != null)
        {
            if (playerState.GetComponent<Player>().defaults)
            {
                LoadDef();
            }
            else
            {
                Load();
            }
        }
        else
        {
            LoadDef();
        }
        /*
        for (int i =0; i < diamonds.Count; i++)
        {
            GameObject clone = Instantiate(diamond, diamonds[i].position, Quaternion.identity);
            clone.transform.parent = spots.transform;
        }*/
    }
    public void Load()
    {
        diamonds.Clear();
        float [,] dpos = playerState.GetComponent<Player>().diamondPosition;
        for (int i =0; i < playerState.GetComponent<Player>().diamondsLeft; i++)
        {
            GameObject clone = Instantiate(diamond,new Vector3(dpos[i,0], dpos[i, 1], dpos[i, 2]), transform.rotation);
            clone.transform.parent = parent.transform;
            diamonds.Add(clone.transform);
        }
    }
    public void LoadDef()
    {
        foreach (Transform spot in spots)
        {
            Quaternion quaternion = new Quaternion(-45f, 0.0f, 0.0f, 0f);
            GameObject clone = Instantiate(diamond, spot.position,transform.rotation);
            clone.transform.parent = parent.transform;
            diamonds.Add(clone.transform);
            //Destroy(spot.gameObject);
        }
    }
    public List<Transform> DiamondsList()
    {
        diamonds.Clear();
        foreach (Transform spot in parent.transform)
        {
            diamonds.Add(spot);
        }
        return diamonds;
    }
}
