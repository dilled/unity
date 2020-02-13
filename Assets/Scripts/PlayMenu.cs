using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayMenu : MonoBehaviour
{
    public float m_IntervalBetweenAdsInSecondes = 300f;
    public float timePassed;
    public Button adButton;
    public Button adWait;
    public Text sensValue;
    GameObject playerState;
    public bool clicked = false;
    public GameObject gameManager;
    public Slider slider;

    // Start is called before the first frame update
    public void QuitToMenu()
    {
       
        if (playerState != null)
        {
            playerState.GetComponent<Player>().SaveSets();
        }
        Debug.Log("quittomenu");
        SceneManager.LoadScene(0);//, LoadSceneMode.Single);
    }
    
    public void SetPause()
    {
         Time.timeScale = 0f;
    }
    public void UnPause()
    {
        //playerState = GameObject.Find("PlayerState");
        if (playerState != null)
        {
            playerState.GetComponent<Player>().SaveSets();
        }
        Time.timeScale = 1f;
    }
    public void OnValueChanged(float newValue)
    {
        sensValue.text = newValue.ToString();
        //Debug.Log(newValue);
    }
    private void Update()
    {
        /*
        timePassed = Time.time - m_LatestDisplayedAdTime;
        if (!clicked
                 || timePassed >= m_IntervalBetweenAdsInSecondes)
        {
            adButton.gameObject.SetActive(true);
            adWait.gameObject.SetActive(false);
            clicked = false;
            //playerState.GetComponent<Player>().clicked = false;
            //gameManager.GetComponent<PlaytimeAds>().clicked = false;
        }
        else
        {
            clicked = true;
            adButton.gameObject.SetActive(false);
            adWait.gameObject.SetActive(true);
        }*/
        }
    private float m_LatestDisplayedAdTime ;

    private void Start()
    {
        
        
    }

}
