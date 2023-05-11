using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnaMenu_Manager : MonoBehaviour
{   
    void Start()
    {        
        if (PlayerPrefs.HasKey("Level"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        }else
        {
            PlayerPrefs.SetInt("Level", 1);
            SceneManager.LoadScene(1);
        }
    }    
}
