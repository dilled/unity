using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{

    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {/*
        var resolution = Screen.currentResolution;
        var height = (int)(resolution.height * 0.65f);
        var width = (int)(resolution.width * 0.65f);
        Screen.SetResolution(height, width, true);
        */
     //   Time.timeScale = 0;
        if (instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    #endregion

    public GameObject player;
    public Transform playerPos;
    public Transform startPos;
    public Transform homePos;
    public Transform scubaPos;
    public Transform startPosDef;
    public int scene;
    
    

    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
