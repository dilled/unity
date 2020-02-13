
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : Interactable
{
    public Item item;
    public GameObject players;
    public GameObject gameManager;

    public string rewardText;
    public Text rewardtext;

    CharacterStats myStats;

    public override void Interact()
    {
        base.Interact();

        Pickup();
    }
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        players = GameObject.Find("unicorn");
        myStats = players.GetComponent<CharacterStats>();
        if (!item.isDiamond)
        {
            if (!item.isFood)
            {
                if (!item.isStamina)
                {
                    if (!item.isCheckpoint)
                    {
                        gameObject.GetComponentInChildren<Text>().text = item.cost.ToString();
                        //texti.text = item.cost.ToString();
                    }
                }
            }
        }
    
    }
    
    void Pickup()
    {
        
        // Debug.Log("picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
        if (item.isFood)
        {
            //CharacterStats myStats = players.GetComponent<CharacterStats>();
            if (myStats.currentHealth < myStats.maxHealth)
            {
                myStats.EatFood();
                AudioSource.PlayClipAtPoint(item.audio, transform.position);
                gameManager.GetComponent<SceneFood>().EatFood(gameObject);
                //Destroy(gameObject);
            }
        }
        
        if (item.isDiamond)
        {
            //Debug.Log("picking up DDD" + item.name);
            Inventory.instance.AddDiamond();
            AudioSource.PlayClipAtPoint(item.audio, transform.position);
            Destroy(gameObject);
        }
        if (item.isWings)
        {
            //CharacterStats myStats = players.GetComponent<CharacterStats>();
            myStats.AbleFly();
            AudioSource.PlayClipAtPoint(item.audio, transform.position);
           // Debug.Log("enabled " + item.name);
            //Inventory.instance.AddDiamond();
            //Destroy(gameObject);
        }
        if (item.isFlosh)
        {
            
            if (Inventory.instance.diamonds >= item.cost)
            {
                rewardtext.gameObject.SetActive(true);
                rewardtext.text = rewardText.ToString();
                Inventory.instance.RemoveDiamond(item.cost);
                Inventory.instance.items.Add(item);
               // CharacterStats myStats = players.GetComponent<CharacterStats>();
                myStats.AbleFlosh();
                AudioSource.PlayClipAtPoint(item.audio, transform.position);
                // Debug.Log("enabled " + item.name);
                //Inventory.instance.AddDiamond();
                
               // Destroy(gameObject);
            }
        }
        if (item.isScuba)
        {
            //CharacterStats myStats = players.GetComponent<CharacterStats>();
            myStats.AbleSwim();
            AudioSource.PlayClipAtPoint(item.audio, transform.position);
           // Debug.Log("enabled " + item.name);
            //Inventory.instance.AddDiamond();
            Destroy(gameObject);
        }
        if (item.isStamina)
        {
           //Debug.Log("picking up DDD" + item.name);
            //CharacterStats myStats = players.GetComponent<CharacterStats>();

            myStats.CollectStamina();
        }
        if (item.isCheckpoint)
        {
            Debug.Log("picking up DDD" + item.name);
           // CharacterStats myStats = players.GetComponent<CharacterStats>();

            myStats.Checkpoint();
           // Destroy(gameObject);
        }
    }

    
}
