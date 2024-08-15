using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseController : MonoBehaviour
{
    public GameObject pauseScreen; //pause menu screen
    public bool ispaused = false;

    // Update is called once per frame
    void Update()
    {
         
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            pause();
        }
    
    }

    public void pause()
    {
        if (ispaused == false)
        {
            Cursor.lockState = CursorLockMode.None; //adds curser back
            pauseScreen.SetActive(true); //sets pause screen to true
            Time.timeScale = 0; //freezes
            ispaused = true;
        }

        else
        {
            Cursor.lockState = CursorLockMode.Locked; //removes curser  
            pauseScreen.SetActive(false); //removes pause screen
            Time.timeScale = 1; //unfreezes
            ispaused = false;

        }

    }

    public void onScreenResumeButton() //for on screen resume button in pause menu
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("unpasued");
        ispaused = false;
    }


    public void onScreenMenuButton() //for on screen menu button in pause menu
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); //takes back to previous scene (menu)
        
    }

}
