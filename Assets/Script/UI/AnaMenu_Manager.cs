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

    public void Start()
    {
        LoadingBarFill.fillAmount = 0.75f;

        string sceneName="MainScene";
        if (PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.GetInt("Level");
        }
        else
        {
            PlayerPrefs.SetInt("Level", 1);
           
        }

        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(0.5f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBarFill.fillAmount = progressValue;

            int percentage = (int)(progressValue * 100f);
            loading.text = percentage.ToString("D2") + "%";

            yield return null;
        }
    }
}
