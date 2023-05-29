﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [Header("---LEVEL TEMEL OBJELERİ")]
    [SerializeField] public GameObject Player;
    [SerializeField] private float PlatformSpeed=15f;
    [SerializeField] private GameObject Pota;
    public GameObject Top;
    
    
    
    [SerializeField] private GameObject[] OzellikOlusmaNoktalari;
    [SerializeField] GameObject[] ozellikler;

    [SerializeField] private AudioSource[] Sesler;
    [SerializeField] private ParticleSystem[] Efektler;

    [Header("---Game Bonus")]
    [SerializeField] private float timeLeftHoop=5f;
    [SerializeField] private bool timeLeftHoopGrow=false;
    [SerializeField] private float timeLeftPlatform=5f;
    [SerializeField] private bool timeLeftPlatformSmall=false;
    [SerializeField] private float timeLeftBall=5f;
    [SerializeField] private bool timeLeftBallSmall=false;


    [Header("---Game Object Settings")]
    [SerializeField] private Vector3 smallBallScale =new Vector3(15f,15f,15f);
    [SerializeField] private Vector3 defaultBallScale =new Vector3(25f,25f,25f);
    
    [SerializeField] private Vector3 growHoopScale =new Vector3(75f,75f,75f);
    [SerializeField] private Vector3 defaultHoopScale =new Vector3(55f,55f,55f);
    
    [SerializeField] private Vector3 defaultPlayerScale =new Vector3(1f,1f,1f);
    [SerializeField] private Vector3 smallPlayerScale =new Vector3(0.5f,1f,1f);
   


    [Header("---Game UI")]
    [SerializeField] private Image[] GorevGorselleri;
    [SerializeField] private Sprite GorevTamamSprite;
    [SerializeField] private int AtilmasiGerekenTop;
    [SerializeField] private GameObject[] Paneller;
    [SerializeField] private TextMeshProUGUI LevelAd;


    
    [Header("---Menu UI")]
    [SerializeField] public  bool isGameStart=true;
    [SerializeField] private GameObject LevelPanel;
    [SerializeField] private GameObject PlayerBase;
    [SerializeField] private GameObject[] StartPanel;
    [SerializeField] private TMP_Text Count;
    [SerializeField] public GameObject FailPanel;


      
    [Header("---ADMOB")]

     public GameObject RewardContinueButton;
     [SerializeField] private GameObject Admob;
    


    private GameObject ozellik=null;
    int BasketSayisi;
    float ParmakPozX;
    void Start()
    {
        
        Time.timeScale=1;

        isGameStart=true;
        
        ChangeHoopPosition();
        
        if( PlayerPrefs.GetInt("Level")==0){
            PlayerPrefs.SetInt("Level",1);
        }
        LevelAd.text = "LEVEL : "+ PlayerPrefs.GetInt("Level");
       
        if(PlayerPrefs.GetInt("Level")<3){
            // Pota.transform.localScale=new Vector3(90f, 90f, 90f);
            AtilmasiGerekenTop=PlayerPrefs.GetInt("Level");
            for (int i = 0; i < PlayerPrefs.GetInt("Level") ; i++)
        {
            GorevGorselleri[i].gameObject.SetActive(true);
        }
        }

        else{
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

     ozellik = Instantiate(selectedOzellik, selectedNokta.transform.position, Quaternion.identity);
    ozellik.SetActive(true);
       if(BasketSayisi == AtilmasiGerekenTop ){
        ozellik.SetActive(false);
       }
    }
    
        


    void Update()
    {   
        
        if (Time.timeScale!=0)
        {


            if (Input.touchCount>0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 TouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x,touch.position.y,10));

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        ParmakPozX = TouchPosition.x - Player.transform.position.x;
                        break;
                    case TouchPhase.Moved:
                        if(Player.transform.localScale.x>0.75f){
                            if (TouchPosition.x - ParmakPozX > -1.1f && TouchPosition.x - ParmakPozX < 1.1f)
                        {
                            Player.transform.position = Vector3.Lerp(Player.transform.position, new Vector3(TouchPosition.x - ParmakPozX, Player.transform.position.y, Player.transform.position.z), PlatformSpeed);
                        }
                        }
                        else{
                            if (TouchPosition.x - ParmakPozX > -1.55f && TouchPosition.x - ParmakPozX < 1.55f)
                        {
                            Player.transform.position = Vector3.Lerp(Player.transform.position, new Vector3(TouchPosition.x - ParmakPozX, Player.transform.position.y, Player.transform.position.z), PlatformSpeed);
                        }
                        }
                        
                        break;
                }
            }
        } 
        

        if(timeLeftHoopGrow){
        timeLeftHoop-=Time.deltaTime;
        if(timeLeftHoop<0){
            Pota.transform.localScale = defaultHoopScale;
            timeLeftHoopGrow=false;
            timeLeftHoop=5f;
        }        
         }
         if(timeLeftBallSmall){
        timeLeftBall-=Time.deltaTime;
        if(timeLeftBall<0){
            Top.transform.localScale=defaultBallScale;
            timeLeftBallSmall=false;
            timeLeftBall=5f;
        }        
         }
         if(timeLeftPlatformSmall){
        timeLeftPlatform-=Time.deltaTime;
        if(timeLeftPlatform<0){
            Player.transform.localScale=defaultPlayerScale;
            timeLeftPlatformSmall=false;
            timeLeftPlatform=5f;
        }        
        }
    }
    public void Basket(Vector3 Poz)
    {
        BasketSayisi++;
        GorevGorselleri[BasketSayisi - 1].sprite = GorevTamamSprite;
        Efektler[0].transform.position = Poz;
        Efektler[0].gameObject.SetActive(true);
        Sesler[1].Play();
        ChangeHoopPosition();
        int randomCount = Random.Range(0, 5);
        Debug.Log(randomCount);
        if (BasketSayisi == AtilmasiGerekenTop)
        {
            isGameStart=false;
            Kazandin();
        }

        if (randomCount>2)
        {   
            if(PlayerPrefs.GetInt("Level")>2){
            OzellikOlussun();}
        }
    }
    public void Kaybettin()
    {
        Sesler[2].Play();
        Paneller[2].SetActive(true);
        if(ozellik!=null){
            ozellik.SetActive(false);
        }
        Top.transform.position=new Vector3(0,1f,-0.119999997f);
     PlayerBase.transform.position=new Vector3 (0f,-2.78f,0f);
        isGameStart=false;
        if(isGameStart==false){
            
            PlayerBase.SetActive(false);
            LevelPanel.SetActive(false);
            Time.timeScale = 0;
        }

        

    }
    void Kazandin()
    {
        Sesler[3].Play();
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level")+1);
        Paneller[1].SetActive(true);
        if(ozellik!=null){
            ozellik.SetActive(false);
        }
        if(isGameStart==false){
            LevelPanel.SetActive(false);
            PlayerBase.SetActive(false);
            Time.timeScale = 0;
        }
    }


    public void PotaBuyut(Vector3 Poz)
    {
        Efektler[1].transform.position = Poz;
        Efektler[1].gameObject.SetActive(true);
        Sesler[0].Play();
        Pota.transform.localScale = growHoopScale;
        timeLeftHoopGrow=true;

    }
     public void TopKucult(Vector3 Pos)
    {
        Efektler[1].transform.position = Pos;
        Efektler[1].gameObject.SetActive(true);
        Sesler[0].Play();
        Top.transform.localScale = smallBallScale;
        timeLeftBallSmall=true;
    }
     public void PlatformKucult(Vector3 Pos)
    {
        Efektler[1].transform.position = Pos;
        Efektler[1].gameObject.SetActive(true);
        Sesler[0].Play();
        Player.transform.localScale = new Vector3(.5f,Player.transform.localScale.y,Player.transform.localScale.z);
        timeLeftPlatformSmall=true;
    }
void ChangeHoopPosition()
{
    Sesler[0].Play();

    if (BasketSayisi == 0)
    {
        Vector3 newPosition = new Vector3(Random.Range(-1.2f, 1.2f), Random.Range(1.5f, 3.10f), Pota.transform.position.z);
        Pota.transform.position = newPosition;
    }
    else
    {   Pota.SetActive(false);
        StartCoroutine(DelayedHoopPositionChange());
    }
}

IEnumerator DelayedHoopPositionChange()
{
    yield return new WaitForSeconds(1f); // Delay for 2 seconds
    Pota.SetActive(true);
    Vector3 newPosition = new Vector3(Random.Range(-1.2f, 1.2f), Random.Range(-1.5f, 3.10f), Pota.transform.position.z);
    Pota.transform.position = newPosition;
}

    GecisReklami _gecisReklami=new GecisReklami();
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
