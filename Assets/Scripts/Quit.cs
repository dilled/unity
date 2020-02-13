using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    GameObject playerState;
    public GameObject canvasMainMenu;
    public bool active = false;

    public void QuitGame()
    {
        Debug.Log("Quit game");
        //SceneManager.LoadScene(0);
    }
    public void QuitSave()
    {
        Debug.Log("Quit game");
        playerState = GameObject.Find("PlayerState");
       // playerState.GetComponent<Player>().SaveSets();
        SceneManager.LoadScene(0);
    }
    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            active = !active;
            canvasMainMenu.SetActive(active);
        }
    }
}
