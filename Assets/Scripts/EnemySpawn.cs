using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject parent;
    public Transform spots;
    public GameObject babyDino;

    public List<Transform> enemys;
    public GameObject player;
    public GameObject friend;
    // Start is called before the first frame update

    public void CheckEnemies()
    {
        //enemies = enemys.GetComponentsInChildren<EnemyController>();

        foreach (Transform enemy in enemys)
        {
            if (enemy != null)
            {
                float dist = Vector3.Distance(enemy.transform.position, player.transform.position);
                float dist2 = Vector3.Distance(enemy.transform.position, friend.transform.position);
                if (dist > 80f && dist2 > 80f)
                {
                    enemy.transform.gameObject.SetActive(false);
                   // GameObject clone = Instantiate(babyDino, enemy.position, transform.rotation);
                    //enemys.Add(clone.transform);
                }
                else
                {
                    enemy.transform.gameObject.SetActive(true);
                    //GameObject clone = Instantiate(babyDino, enemy.position, transform.rotation);
                    //enemys.Add(clone.transform);
                }
            }

        }
        //Debug.Log(enemi);
    }
    public void LoadDef()
    {
        foreach (Transform spot in spots)
        {
            //Quaternion quaternion = new Quaternion(0.0f, 0.0f, 0.0f, 0f);
            GameObject clone = Instantiate(babyDino, spot.position, transform.rotation);
            clone.transform.parent = parent.transform;
            enemys.Add(clone.transform);
            //Destroy(spot.gameObject);
        }
    }
    // Update is called once per frame
    private void Start()
    {
        LoadDef();
    }
    void Update()
    {
        //CheckEnemies();
    }
}