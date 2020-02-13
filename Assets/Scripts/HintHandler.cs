using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintHandler : MonoBehaviour
{
    
    public float distance = 15f;
    public GameObject player;
    public GameObject hint;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("unicorn");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance2 = Vector3.Distance(player.transform.position, transform.position);
        if (distance2 < distance)
        {
            hint.SetActive(true);
            gameObject.GetComponent<HintHandler>().enabled = false;
        }
    }
}
