using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    public List<Transform> enemys;
    //public GameObject[] enemys;
    public GameObject player;
    public GameObject friend;
    // Start is called before the first frame update
    
    public void CheckEnemies()
    {
        //enemies = enemys.GetComponentsInChildren<EnemyController>();

        foreach (Transform enemy in transform)
        {
            if (enemy != null)
            {
                
                float dist = Vector3.Distance(enemy.transform.position, player.transform.position);
                float dist2 = Vector3.Distance(enemy.transform.position, friend.transform.position);
                if (dist > 80f && dist2 > 80f)
                {
                    enemy.transform.gameObject.SetActive(false);
                }
                else
                {
                    enemy.transform.gameObject.SetActive(true);
                }
            }

        }
        //Debug.Log(enemi);
    }
    private void Start()
    {
        foreach (Transform enemy in transform)
        {
            enemys.Add(enemy.transform);
        }
    }
    // Update is called once per frame
    void Update()
    {
        CheckEnemies();
    }
}
