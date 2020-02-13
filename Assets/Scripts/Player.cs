using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    #region Singleton

    public static Player instance;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        /*
        position[0] = GetComponent<PlayerManager>().startPos.position.x;
        position[1] = GetComponent<PlayerManager>().startPos.position.y;
        position[2] = GetComponent<PlayerManager>().startPos.position.z;
        diamonds = GetComponent<Inventory>().diamonds;
        ableSwim = player.GetComponent<CharacterStats>().ableSwim;
        maxHealth = player.GetComponent<CharacterStats>().maxHealth;
        maxStamina = player.GetComponent<CharacterStats>().maxStamina;

        currentQuest = friend.GetComponent<Quests>().currentQuest;
    */
    }
    #endregion
    public Transform start;

    public GameObject player;
    public float maxHealth;
    public float maxStamina;
    public float maxDive;
    public bool ableSwim;
    public bool ableFlosh;
    public int diamonds;
    public float[] position;
    public GameObject continueButton;

    public GameObject friend;
    public int currentQuest;
    public Vector3 startPosition;

    public GameObject gameManager;
    public bool defaults = true;

    public Transform diamondClones;
    public int diamondsLeft;
    public float[,] diamondPosition;
    public List<Transform> diamondsList;

    public float timePassed;
    public float timePassedWait;
    public bool clicked;
    public float sensitivity;

    public void SavePlayer(Transform player)
    {
        /*position[0] = GetComponent<PlayerManager>().startPos.position.x;
        position[1] = GetComponent<PlayerManager>().startPos.position.y;
        position[2] = GetComponent<PlayerManager>().startPos.position.z;
        */
        diamondClones = GameObject.Find("DiamondClones").transform;

        friend = GameObject.Find("UnicornFriend");
        gameManager = GameObject.Find("GameManager");
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        diamonds = gameManager.GetComponent<Inventory>().diamonds;
        ableSwim = player.GetComponent<CharacterStats>().ableSwim;
        ableFlosh = player.GetComponent<CharacterStats>().ableFlosh;
        maxHealth = player.GetComponent<CharacterStats>().maxHealth;
        maxStamina = player.GetComponent<CharacterStats>().maxStamina;
        maxDive = player.GetComponent<CharacterStats>().maxDive;
        if (friend != null)
        {
            currentQuest = friend.GetComponent<Quests>().currentQuest;
        }
        diamondsList = gameManager.GetComponent<SceneDiamonds>().DiamondsList();
        diamondsLeft = diamondsList.Count;
        //int i = 0;
        //foreach (Transform spot in diamondsList)
        diamondPosition = new float[diamondsList.Count,3];
        for (int i = 0; i < diamondsList.Count; i++)
        {
            //Debug.Log(diamondsList[i].position.x + i);

            diamondPosition[i, 0] = diamondsList[i].position.x;
            diamondPosition[i, 1] = diamondsList[i].position.y;
            diamondPosition[i, 2] = diamondsList[i].position.z;
            //diamondPosition[i][1] = spot.position.y;
            //diamondPosition[i][2] = spot.position.z;


        }
        SaveSystem.SavePlayer(this);
       // Debug.Log(diamondPosition.Length);
        //continueButton.SetActive(true);
    }
    public void LoadPlayer()
    {
        Time.timeScale = 1f;
        //Debug.Log("load");
        PlayerData data = SaveSystem.LoadPlayer();
        defaults = false;
        maxHealth = data.maxHealth;
        maxStamina = data.maxStamina;
        maxDive = data.maxDive;
        ableSwim = data.ableSwim;
        ableFlosh = data.ableFlosh;
        diamonds = data.diamonds;
        //gameManager.GetComponent<Inventory>().diamonds = diamonds;
        // position[0] = data.position[0];
        // position[1] = data.position[1];
        // position[2] = data.position[2];
        position = data.position;
        currentQuest = data.currentQuest; 

        
        startPosition.x = data.position[0];
        startPosition.y = data.position[1];
        startPosition.z = data.position[2];

        diamondPosition = data.diamondPosition;
        diamondsLeft = data.diamondsLeft;
        PlayerSets dat = SaveSystem.LoadSets();
        timePassed = dat.timePassed;
        timePassedWait = dat.timePassedWait;
        clicked = dat.clicked;
        sensitivity = dat.sensitivity;
        /*
        player.GetComponent<CharacterStats>().maxHealth = data.maxHealth;
        player.gameObject.GetComponent<CharacterStats>().maxStamina = data.maxStamina;
        player.gameObject.GetComponent<CharacterStats>().ableSwim = data.ableSwim;
        GetComponent<Inventory>().diamonds = data.diamonds;
        //player.transform.position = startPosition;
        PlayerManager.instance.startPos.position = startPosition;
        //player.GetComponent<PlayerController>().Teleport(data.startpos);
        friend.GetComponent<Quests>().currentQuest = data.currentQuest;
    */
    }
    public void LoadPlayerDef()
    {
        Time.timeScale = 1f;
        defaults = true;

       // Debug.Log("load def");
        maxHealth = 100f;
        maxStamina = 100f;
        maxDive = 0f;
        ableSwim = false;
        ableFlosh = false;
        diamonds = 0;

        currentQuest = 0;
        /*
        position[0] = 455.24f;
        position[1] = 6.748f; // GetComponent<PlayerManager>().startPosDef.position.y;
        position[2] = 251.90f; // GetComponent<PlayerManager>().startPosDef.position.z;
        */
        /*startPosition.x = position[0];
        startPosition.y = position[1];
        startPosition.z = position[2];
        */
        startPosition= start.transform.position;
        PlayerSets data = SaveSystem.LoadSets();
        if (data != null)
        {
            timePassed = data.timePassed;
            timePassedWait = data.timePassedWait;
            clicked = data.clicked;
            sensitivity = data.sensitivity;
        }
        else
        {
            timePassed = 0f;
            timePassedWait = 0f;
            clicked = false;
            sensitivity = 3f;
        }
            /*
        PlayerManager.instance.startPos.position = PlayerManager.instance.startPosDef.position;
        player.GetComponent<CharacterStats>().maxHealth = maxHealth;
        player.gameObject.GetComponent<CharacterStats>().maxStamina = maxStamina;
        player.gameObject.GetComponent<CharacterStats>().ableSwim = ableSwim;
        GetComponent<Inventory>().diamonds = diamonds;
        //player.transform.position = startPosition;
        //PlayerManager.instance.startPos.position = startPosition;
        //player.GetComponent<PlayerController>().Teleport(data.startpos);
    */
    }
    public void SaveSets()
    {
        
        
    }
    public void LoadSets()
    {
    }
}
