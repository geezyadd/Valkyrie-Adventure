using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartMenuController : MonoBehaviour
{
    
    [SerializeField] private List<Button> _levelsButton;
    public void FirstLevel() 
    {
        SceneManager.LoadScene("firstLevel");
    }
    public void SecondLevel() 
    {
        SceneManager.LoadScene("secondLevel");
    }

    

}
