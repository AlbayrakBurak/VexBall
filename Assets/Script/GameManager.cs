using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.Android;

public class GameManager : MonoBehaviour
{
    [Header("---LEVEL TEMEL OBJELERİ")]
    [SerializeField] public GameObject Player;
    [SerializeField] private float PlatformSpeed = 15f;
    [SerializeField] private GameObject Pota;
    public GameObject Top;
    public GameObject cubePrefab; // Küp prefabı

    public Vector3 potaObstacle;

    [SerializeField] private GameObject[] OzellikOlusmaNoktalari;
    [SerializeField] GameObject[] ozellikler;

    [SerializeField] private AudioSource[] Sesler;
    [SerializeField] private ParticleSystem[] Efektler;

    [Header("---Game Bonus")]
    [SerializeField] private float timeLeft = 5f;

    [SerializeField] private float timeLeftHoop = 5f;
    [SerializeField] private bool timeLeftHoopGrow = false;
    [SerializeField] private float timeLeftPlatform = 5f;
    [SerializeField] private bool timeLeftPlatformSmall = false;
    [SerializeField] private float timeLeftBall = 5f;
    [SerializeField] private bool timeLeftBallSmall = false;

    [Header("---Game Object Settings")]
    [SerializeField] private Vector3 smallBallScale = new Vector3(15f, 15f, 15f);
    [SerializeField] private Vector3 defaultBallScale = new Vector3(25f, 25f, 25f);

    [SerializeField] private Vector3 growHoopScale = new Vector3(75f, 75f, 75f);
    [SerializeField] private Vector3 defaultHoopScale = new Vector3(55f, 55f, 55f);

    [SerializeField] private Vector3 defaultPlayerScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 smallPlayerScale = new Vector3(0.5f, 1f, 1f);


    [Header("---Game UI")]
    [SerializeField] private Image[] GorevGorselleri;
    [SerializeField] private Sprite AtilanBasketSprite;
    [SerializeField] private int AtilmasiGerekenTop;
    [SerializeField] private GameObject[] Paneller;
    [SerializeField] private TextMeshProUGUI LevelAd;


    [Header("---Menu UI")]
    [SerializeField] public bool isGameStart = true;
    [SerializeField] private GameObject LevelPanel;
    [SerializeField] private GameObject PlayerBase;
    [SerializeField] private GameObject[] StartPanel;
    [SerializeField] private TMP_Text Count;
    [SerializeField] public GameObject FailPanel;
    [Header("---ADMOB")]
    public GameObject RewardContinueButton;
    [SerializeField] private GameObject Admob;
    private GameObject ozellik = null;
    int BasketSayisi;
    float ParmakPozX;
    void Start()
    {
        //PlayerPrefs.SetInt("Level",150);

        Time.timeScale = 1;

        isGameStart = true;

        ChangeHoopPosition();

        if (PlayerPrefs.GetInt("Level") == 0)
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        LevelAd.text = "LEVEL : " + PlayerPrefs.GetInt("Level");

        if (PlayerPrefs.GetInt("Level") < 3)
        {
            // Pota.transform.localScale=new Vector3(90f, 90f, 90f);
            AtilmasiGerekenTop = PlayerPrefs.GetInt("Level");
            for (int i = 0; i < PlayerPrefs.GetInt("Level"); i++)
            {
                GorevGorselleri[i].gameObject.SetActive(true);
            }
        }

        else
        {
            for (int i = 0; i < AtilmasiGerekenTop; i++)
            {
                GorevGorselleri[i].gameObject.SetActive(true);

            }
        }
         
        
    }

    void OzellikOlussun()
    {
        int randomNoktaIndex = Random.Range(0, OzellikOlusmaNoktalari.Length);
        GameObject selectedNokta = OzellikOlusmaNoktalari[randomNoktaIndex];
        int randomOzellikIndex = Random.Range(0, ozellikler.Length);
        GameObject selectedOzellik = ozellikler[randomOzellikIndex];

        if (ozellik != null)
        {
            Destroy(ozellik);
        }

        ozellik = Instantiate(selectedOzellik, selectedNokta.transform.position, Quaternion.identity);
        ozellik.SetActive(true);

        if (BasketSayisi == AtilmasiGerekenTop)
        {
            ozellik.SetActive(false);
        }
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));

            if (touch.phase == TouchPhase.Began)
            {
                ParmakPozX = touchPosition.x - Player.transform.position.x;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                float screenWidth = Screen.width;
                float screenMinX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
                float screenMaxX = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, 0, 10)).x;

                float margin = 0.125f;
                float minX, maxX;

                if (Player.transform.localScale.x <= 0.5f)
                {
                    minX = screenMinX + margin + 0.55f;
                    maxX = screenMaxX - margin - 0.55f;
                }
                else
                {
                    minX = Player.transform.localScale.x > 0.75f ? screenMinX + margin + 1.1f : screenMinX + margin + 1.55f;
                    maxX = Player.transform.localScale.x > 0.75f ? screenMaxX - margin - 1.1f : screenMaxX - margin - 1.55f;
                }

                float clampedX = Mathf.Clamp(touchPosition.x - ParmakPozX, minX, maxX);
                Vector3 newPosition = new Vector3(clampedX, Player.transform.position.y, Player.transform.position.z);
                Player.transform.position = Vector3.Lerp(Player.transform.position, newPosition, PlatformSpeed);
            }
        }

        UpdateTimer(ref timeLeftHoop, ref timeLeftHoopGrow, Pota, defaultHoopScale);
        UpdateTimer(ref timeLeftBall, ref timeLeftBallSmall, Top, defaultBallScale);
        UpdateTimer(ref timeLeftPlatform, ref timeLeftPlatformSmall, Player, defaultPlayerScale);
    }

    void UpdateTimer(ref float timeLeft, ref bool isTimerRunning, GameObject targetObject, Vector3 defaultScale)
    {
        if (isTimerRunning)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                targetObject.transform.localScale = defaultScale;
                isTimerRunning = false;
            }
        }
    }

   public void Basket(Vector3 poz)
{
     if (Application.platform == RuntimePlatform.Android)
    {   
        Handheld.Vibrate();
    }
    BasketSayisi++;
    GorevGorselleri[BasketSayisi - 1].sprite = AtilanBasketSprite;
    Efektler[0].transform.position = poz;
    Efektler[0].gameObject.SetActive(true);
    Sesler[1].Play();
    ChangeHoopPosition();
    
    int randomCount = Random.Range(0, 5);
    Debug.Log(randomCount);
    
    if (BasketSayisi == AtilmasiGerekenTop)
    {
        isGameStart = false;
        Kazandin();
    }

    if (randomCount > 2 && PlayerPrefs.GetInt("Level") > 2)
    {
        OzellikOlussun();
    }
}

    public void Kaybettin()
{
    Sesler[2].Play();
    Paneller[2].SetActive(true);
    
    if (ozellik != null)
    {
        ozellik.SetActive(false);
    }
    
    Top.transform.position = new Vector3(0, -1.5f, -0.119999997f);
    Top.SetActive(false);
    
    PlayerBase.transform.position = new Vector3(0f, -2.78f, 0f);
    isGameStart = false;
    PlayerBase.SetActive(false);
    LevelPanel.SetActive(false);
    Time.timeScale = 0;
}

    void Kazandin()
{
    Sesler[3].Play();
    
    int currentLevel = PlayerPrefs.GetInt("Level");
    PlayerPrefs.SetInt("Level", currentLevel + 1);
    
    Paneller[1].SetActive(true);
    
    if (ozellik != null)
    {
        ozellik.SetActive(false);
    }
     Top.SetActive(false);
    LevelPanel.SetActive(false);
    PlayerBase.SetActive(false);
    Time.timeScale = 0;
}

   public void PotaBuyut(Vector3 Poz)
{
    SetEfektlerPosition(Poz);
    Efektler[1].gameObject.SetActive(true);
    Sesler[0].Play();
    Pota.transform.localScale = growHoopScale;
    timeLeftHoopGrow = true;
}

public void TopKucult(Vector3 Pos)
{
    SetEfektlerPosition(Pos);
    Efektler[1].gameObject.SetActive(true);
    Sesler[0].Play();
    Top.transform.localScale = smallBallScale;
    timeLeftBallSmall = true;
}

public void PlatformKucult(Vector3 Pos)
{
    SetEfektlerPosition(Pos);
    Efektler[1].gameObject.SetActive(true);
    Sesler[0].Play();
    Player.transform.localScale = new Vector3(0.5f, Player.transform.localScale.y, Player.transform.localScale.z);
    timeLeftPlatformSmall = true;
}

private void SetEfektlerPosition(Vector3 position)
{
    Efektler[1].transform.position = position;
}

    void ChangeHoopPosition()
    {
        Sesler[0].Play();
        
        if (BasketSayisi == 0)
        {
            Vector3 newPosition = new Vector3(Random.Range(-1.2f, 1.2f), Random.Range(2f, 3.5f), Pota.transform.position.z);
            Pota.transform.position = newPosition;
        }
        else
        {
            Pota.SetActive(false);
            StartCoroutine(DelayedHoopPositionChange());
        }
        if(PlayerPrefs.GetInt("Level")>=150){
            potaObstacle =Pota.gameObject.transform.position;
        Instantiate(cubePrefab,potaObstacle,Quaternion.identity); //create the cube last pota position when pota changed position 
        }
    }

    IEnumerator DelayedHoopPositionChange()
    {
        yield return new WaitForSeconds(0.75f); // Delay for 2 seconds
        Pota.SetActive(true);
        Vector3 newPosition = new Vector3(Random.Range(-1.2f, 1.2f), Random.Range(2f, 3.5f), Pota.transform.position.z);
        Pota.transform.position = newPosition;
    }
    GecisReklami _gecisReklami = new GecisReklami();
    public void Butonlarinislemleri(string Deger)
    {
        switch (Deger)
        {
            case "Durdur":
                Time.timeScale = 0;
                Paneller[0].SetActive(true);
                break;
            case "DevamEt":
                Time.timeScale = 1;
                Paneller[0].SetActive(false);
                break;

            case "Tekrar":

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                break;
            case "SonrakiLevel":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                break;
            case "Ayarlar":

                break;

            case "cikis":
                Application.Quit();
                break;

        }

    }
}
