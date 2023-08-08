using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CheckInternet : MonoBehaviour
{
    [Header("--BUTTONS")]
    [SerializeField] public Button playButton;

    [Header("--UI OBJECTS")]
    public GameObject LoadingBar;
    public Image LoadingBarFill;
    public GameObject LoadingScreen;
    public TextMeshProUGUI loading;

    public GameObject SuccessPanel;
    public GameObject ErrorPanel;

    private void Start()
    {
        SuccessPanel.SetActive(true);
        ErrorPanel.SetActive(false);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Play()
    {
        playButton.interactable = false;
        LoadingBar.SetActive(true);
        Loading();
    }

    public void Loading()
    {
        LoadingBarFill.fillAmount = 0.75f;

        string sceneName = "MainScene";
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
