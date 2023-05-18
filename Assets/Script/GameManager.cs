﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("---LEVEL TEMEL OBJELERİ")]
    [SerializeField] private GameObject PlayerBase;
    [SerializeField] private float PlatformSpeed=1f;
    [SerializeField] private GameObject Pota;
    [SerializeField] private GameObject Top;
    
    [SerializeField] private GameObject[] OzellikOlusmaNoktalari;
    [SerializeField] GameObject[] ozellikler;

    [SerializeField] private AudioSource[] Sesler;
    [SerializeField] private ParticleSystem[] Efektler;


    [Header("---UI OBJELERİ")]
    [SerializeField] private Image[] GorevGorselleri;
    [SerializeField] private Sprite GorevTamamSprite;
    [SerializeField] private int AtilmasiGerekenTop;
    [SerializeField] private GameObject[] Paneller;
    [SerializeField] private TextMeshProUGUI LevelAd;
    int BasketSayisi;
    float ParmakPozX;
    void Start()
    {
        LevelAd.text = "LEVEL : "+ PlayerPrefs.GetInt("Level");


        for (int i = 0; i < AtilmasiGerekenTop; i++)
        {
            GorevGorselleri[i].gameObject.SetActive(true);

        }

        // Invoke("OzellikOlussun", 3f);

    }

    void OzellikOlussun()
    {
int randomNoktaIndex = Random.Range(0, OzellikOlusmaNoktalari.Length);
    GameObject selectedNokta = OzellikOlusmaNoktalari[randomNoktaIndex];

    int randomOzellikIndex = Random.Range(0, ozellikler.Length);
    GameObject selectedOzellik = ozellikler[randomOzellikIndex];

    GameObject ozellik = Instantiate(selectedOzellik, selectedNokta.transform.position, Quaternion.identity);
    ozellik.SetActive(true);
       
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
                        ParmakPozX = TouchPosition.x - PlayerBase.transform.position.x;
                        break;
                    case TouchPhase.Moved:
                        if(PlayerBase.transform.localScale.x>0.75f){
                            if (TouchPosition.x - ParmakPozX > -1.1f && TouchPosition.x - ParmakPozX < 1.1f)
                        {
                            PlayerBase.transform.position = Vector3.Lerp(PlayerBase.transform.position, new Vector3(TouchPosition.x - ParmakPozX, PlayerBase.transform.position.y, PlayerBase.transform.position.z), PlatformSpeed);
                        }
                        }
                        else{
                            if (TouchPosition.x - ParmakPozX > -1.55f && TouchPosition.x - ParmakPozX < 1.55f)
                        {
                            PlayerBase.transform.position = Vector3.Lerp(PlayerBase.transform.position, new Vector3(TouchPosition.x - ParmakPozX, PlayerBase.transform.position.y, PlayerBase.transform.position.z), PlatformSpeed);
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
        int randomCount = Random.Range(0, 5);
        if (BasketSayisi == AtilmasiGerekenTop)
        {
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
        Time.timeScale = 0;
    }
    void Kazandin()
    {
        Sesler[3].Play();
        Paneller[1].SetActive(true);
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        Time.timeScale = 0;

    }
    public void PotaBuyut(Vector3 Poz)
    {
        Efektler[1].transform.position = Poz;
        Efektler[1].gameObject.SetActive(true);
        Sesler[0].Play();
        Pota.transform.localScale = new Vector3(55f, 55f, 55f);
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
        PlayerBase.transform.localScale = new Vector3(.5f,PlayerBase.transform.localScale.y,PlayerBase.transform.localScale.z);
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
