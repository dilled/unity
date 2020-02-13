using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/* Base class that player and enemies can derive from to include stats. */

public class CharacterStats : MonoBehaviour
{
    GameObject playerState;
    // Health
    public Image hidingEye;
    public Image healthBar;
    public Image flyBar;
    public Image diveBar;
    public Image staminaBar;
    public Text diamonds;
    int currentDiamonds;
    public Image hiding;
    public Image sleeping;

    public bool ableFlosh;
    public bool ableFly;
    public bool ableSwim;
    public bool tired;

    public float maxStamina = 100;
    public float currentStamina { get; private set; }

    public float maxDive = 0;
    public float currentDive { get; private set; }

    public float maxFly = 100;
    public float currentFly { get; private set; }

    public float maxHealth = 100;
    public float currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;
    PlayerController2 playerController;
    //public RedEye redEye;
    // Set current health to max health
    // when starting the game.
    void Awake()
    {
        //redEye = GetComponent<RedEye>();
        currentStamina = maxStamina;
        currentHealth = maxHealth;
        currentFly = maxFly;
        currentDive = maxDive;
        playerController = GetComponent<PlayerController2>();
        playerState = GameObject.Find("PlayerState");
    }
    public void AbleFly()
    {
        ableFly = true;
       // playerState = GameObject.Find("PlayerState");
       // playerState.GetComponent<Player>().SavePlayer(gameObject.transform);
    }
    public void Checkpoint()
    {
        // playerState = GameObject.Find("PlayerState");
        if (playerState != null)
            playerState.GetComponent<Player>().SavePlayer(gameObject.transform);
    }
    public void AbleSwim()
    {
        ableSwim = true;
        maxDive = 100;
        // playerState = GameObject.Find("PlayerState");
        //if (playerState != null)
          //  playerState.GetComponent<Player>().SavePlayer(gameObject.transform);
    }
    public void AbleFlosh()
    {
        ableFlosh = true;
        // playerState = GameObject.Find("PlayerState");
      //  if (playerState != null)
          //  playerState.GetComponent<Player>().SavePlayer(gameObject.transform);
    }
    public void Diamonds()
    {
        currentDiamonds = Inventory.instance.Diamonds();
        diamonds.text = currentDiamonds.ToString();
       // Debug.Log("nyt" + currentDiamonds);
    }
    // Damage the character
    public void GainFly()
    {/*
        flyBar.transform.parent.gameObject.SetActive(false);
        currentFly = Mathf.Clamp(currentFly, 0, maxFly);
        currentFly += 0.1f;
        flyBar.fillAmount = currentFly / maxFly;
    */}
    public void GainDive()
    {
        diveBar.transform.parent.gameObject.SetActive(false);
        currentDive = Mathf.Clamp(currentDive, 0, maxDive);
        currentDive = maxDive;
        diveBar.fillAmount = currentDive / maxDive;
    }
    public void GainHealth()
    {      
        

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        currentHealth += 0.05f;  
        healthBar.fillAmount = currentHealth / maxHealth;
        GainStamina();
    }
    public void GainStamina()
    {
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        currentStamina += 0.3f;
        staminaBar.fillAmount = currentStamina / maxStamina;
        if (currentStamina > 50f)
        {
            tired = false;
        }
    }
    public void CollectStamina()
    {
        //Debug.Log("collect stamina");
        maxStamina += 50f;
        currentStamina = maxStamina;
       /* if (playerState != null)
        {
            playerState.GetComponent<Player>().SavePlayer(gameObject.transform);
        }*/
    }
    public void Running(float currentSpeed)
    {
        currentStamina -= currentSpeed /20f;
        staminaBar.fillAmount = currentStamina / maxStamina;
        if (currentStamina <= 0)
        {
            tired = true;
        }
    }
    public void EatFood()
    {
        currentHealth += 30f;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        healthBar.fillAmount = currentHealth / maxHealth;

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        currentStamina = maxStamina;
        staminaBar.fillAmount = currentStamina / maxStamina;
    }
    public void Diving()
    {
        diveBar.transform.parent.gameObject.SetActive(true);
        currentDive -= 0.2f;
        diveBar.fillAmount = currentDive / maxDive;
        if (currentDive <= 0)
        {
            TakeDamage(0.3f);
        }
    }
    public void Hiding()
    {
        hiding.transform.parent.gameObject.SetActive(false);
       // sleeping.transform.gameObject.SetActive(true);

    }
    public void NotHiding()
    {
        hiding.transform.parent.gameObject.SetActive(true);
        //sleeping.transform.gameObject.SetActive(false);

    }
    public void Flying()
    {
        flyBar.transform.parent.gameObject.SetActive(true);
        currentFly -= 0.2f;
        flyBar.fillAmount = currentFly / maxFly;
    }

    public void TakeDamage(float damage)
    {
        // Subtract the armor value
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        // Damage the character
        currentHealth -= damage;
        //healthBar.fillAmount =  currentHealth / maxHealth;
        if (healthBar != null)
            healthBar.fillAmount = currentHealth / maxHealth;
        Debug.Log(transform.name + " takes " + damage + " damage.");
        //Debug.Log(currentHealth+"  "+ maxHealth);
        // If health reaches zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void StartFirst()
    {
        StartCoroutine(StartFirst2());
        
    }
    public virtual void Born()
    {
        currentHealth = maxHealth;
    }
    public virtual void Die()
    {
        // Die in some way
        // This method is meant to be overwritten
        Debug.Log(transform.name + " died.");
        if (transform.name == "unicorn")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //Debug.Log(PlayerManager.instance.startPos.position);

           // StartCoroutine(StartAgain());
            
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public IEnumerator StartFirst2()
    {
        yield return new WaitForSeconds(1f);
        
        playerController.Teleport(PlayerManager.instance.startPosDef);
    }
    public IEnumerator StartAgain()
    {
        yield return new WaitForSeconds(1f);
        if (currentHealth < maxHealth)
        {
            GameObject enemy = GameObject.Find("Enemy");
            Patrol2[] patrol2 = enemy.GetComponentsInChildren<Patrol2>();
            EnemyController[] enemyCont = enemy.GetComponentsInChildren<EnemyController>();
            foreach (Patrol2 patrol in patrol2)
            {
                
                patrol.NotAttack();
            }
            foreach (EnemyController enemyc in enemyCont)
            {

                enemyc.NotAttack();
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            /*playerController.PlayAgain();
            currentHealth = maxHealth;
            currentStamina = maxStamina;
            healthBar.fillAmount = currentHealth / maxHealth;
            //hidingEye.gameObject.SetActive(false);
            hidingEye.fillAmount = 0;
    */    
    }
        //playerController.Teleport(PlayerManager.instance.startPos);
    }
}
