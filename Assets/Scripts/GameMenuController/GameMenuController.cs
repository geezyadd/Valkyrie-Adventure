using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    public void PauseOn() 
    {
        Time.timeScale = 0f;
    }
    public void PauseOff() 
    {
        Time.timeScale = 1f;
    }
    public void GoToStartMenu() 
    {
        SceneManager.LoadScene("StartMenuScene");
    }
}
