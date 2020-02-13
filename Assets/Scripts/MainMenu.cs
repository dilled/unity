using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public GameObject player;
    public GameObject continueButton;
    GameObject playerState;

    private void Awake()
    {
        string path = Application.persistentDataPath + "/unicornpc.sav";
        if (File.Exists(path))
        {
            continueButton.SetActive(true);
        }
        playerState = GameObject.Find("PlayerState");
    }

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //player.GetComponent<PlayerController>().Teleport(PlayerManager.instance.startPosDef);
        //SceneManager.LoadScene(0);
      //  Debug.Log("new game");
        playerState.GetComponent<Player>().LoadPlayerDef();
        //player.GetComponent<CharacterStats>().StartFirst();
       // UnPause();
        //StartCoroutine(UnPaused());
        //UnPause();
        //Invoke("UnPause",3);
    }

    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       // Debug.Log("continue");
        playerState.GetComponent<Player>().LoadPlayer();
        //player.transform.position = PlayerManager.instance.startPos.position;
        //player.GetComponent<PlayerController>().Teleport(PlayerManager.instance.startPos);
        //   player.GetComponent<CharacterStats>().Die();
        // UnPause();
        //StartCoroutine(UnPaused());
        // SceneManager.LoadScene(1);
        //Invoke("UnPause", 3);
    }
    public void QuitGame()
    {
       // Debug.Log("quit");
        Application.Quit();
    }
    
    public void SetPause()
    {
       // Time.timeScale = 0;
    }
    public void UnPause()
    {
        Time.timeScale = 1;
    }
    public IEnumerator UnPaused()
    {
        yield return new WaitForSeconds(3f);
       // UnPause();
    }
}
