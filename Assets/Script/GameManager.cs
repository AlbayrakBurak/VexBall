using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [Header("---LEVEL TEMEL OBJELERİ")]
    [SerializeField] private GameObject Player;
    [SerializeField] private float PlatformSpeed=15f;
    [SerializeField] private GameObject Pota;
    [SerializeField] private GameObject Top;
    
    [SerializeField] private GameObject[] OzellikOlusmaNoktalari;
    [SerializeField] GameObject[] ozellikler;

    [SerializeField] private AudioSource[] Sesler;
    [SerializeField] private ParticleSystem[] Efektler;


    [Header("---Game UI")]
    [SerializeField] private Image[] GorevGorselleri;
    [SerializeField] private Sprite GorevTamamSprite;
    [SerializeField] private int AtilmasiGerekenTop;
    [SerializeField] private GameObject[] Paneller;
    [SerializeField] private TextMeshProUGUI LevelAd;

    
    [Header("---Menu UI")]
    [SerializeField] private bool isGameStart=true;
    [SerializeField] private GameObject LevelPanel;
    [SerializeField] private GameObject PlayerBase;
    [SerializeField] private GameObject[] StartPanel;

    private GameObject ozellik=null;
    int BasketSayisi;
    float ParmakPozX;
    void Start()
    {
        isGameStart=true;
        if( PlayerPrefs.GetInt("Level")==0){
            PlayerPrefs.SetInt("Level",1);
        }
        LevelAd.text = "LEVEL : "+ PlayerPrefs.GetInt("Level");
       
        if(PlayerPrefs.GetInt("Level")<3){
            Pota.transform.localScale=new Vector3(90f, 90f, 90f);
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
            OzellikOlussun();
        }
    }
    public void Kaybettin()
    {
        Sesler[2].Play();
        Paneller[2].SetActive(true);
        if(ozellik!=null){
            ozellik.SetActive(false);
        }
        isGameStart=false;
        if(isGameStart==false){
            
            PlayerBase.SetActive(false);
            LevelPanel.SetActive(false);
        }
        Time.timeScale = 0;
        

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

        }
        Time.timeScale = 0;
         
        
        

    }
    public void PotaBuyut(Vector3 Poz)
    {
        Efektler[1].transform.position = Poz;
        Efektler[1].gameObject.SetActive(true);
        Sesler[0].Play();
        Pota.transform.localScale = new Vector3(75f, 75f, 75f);
    }
     public void TopKucult(Vector3 Pos)
    {
        Efektler[1].transform.position = Pos;
        Efektler[1].gameObject.SetActive(true);
        Sesler[0].Play();
        Top.transform.localScale = new Vector3(20f,20f,20f);
    }
     public void PlatformKucult(Vector3 Pos)
    {
        Efektler[1].transform.position = Pos;
        Efektler[1].gameObject.SetActive(true);
        Sesler[0].Play();
        Player.transform.localScale = new Vector3(.5f,Player.transform.localScale.y,Player.transform.localScale.z);
    }

      void ChangeHoopPosition()
    {
        Sesler[0].Play();
        Vector3 newPosition = new Vector3(Random.Range(-1.8f, 1.8f), Random.Range(-2f, 3f), Pota.transform.position.z);
        Pota.transform.position=newPosition;
    }
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
                // ayarlar paneli yapılabilir. sana bırakıyorum
                break;

            case "cikis":
                Application.Quit(); // emin msin paneli ypaıalbilir. Run controlde yaptık.
                break;

        }

    }
}
