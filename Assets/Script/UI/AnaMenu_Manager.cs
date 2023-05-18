using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AnaMenu_Manager : MonoBehaviour
{   
   public Image LoadingBarFill;
    public GameObject LoadingScreen;
    public TextMeshProUGUI loading;

    
    public void Start(){
        LoadingBarFill.fillAmount=0.75f;
        
       int sceneIndex;
        if (PlayerPrefs.HasKey("Level"))
        {
            sceneIndex=PlayerPrefs.GetInt("Level");
        }else
        {
            PlayerPrefs.SetInt("Level", 1);
             sceneIndex=PlayerPrefs.GetInt("Level");;
        }

        StartCoroutine(LoadSceneAsync(sceneIndex));
    }

    IEnumerator LoadSceneAsync (int sceneIndex) {
       
       yield return new WaitForSeconds( 0.5f );
        
        AsyncOperation operation =SceneManager.LoadSceneAsync(sceneIndex);
        LoadingScreen.SetActive(true);
        

        while(!operation.isDone){
            float progressValue=Mathf.Clamp01(operation.progress/.9f);

           LoadingBarFill.fillAmount=progressValue;
           
           loading.text=progressValue*100f+"%";
            yield return null;
        }
    }
    
}
