using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCustomizeManager : MonoBehaviour
{
    public GameObject platform1; // İlk platform objesi
    public GameObject platform2; // İkinci platform objesi
    public GameObject platform3; // Üçüncü platform objesi
    public Renderer ballRenderer; // Renderer referansı eklendi
    public TrailRenderer topTrailRenderer; // Topun TrailRenderer bileşeni
    public Material[] BallMaterials;
    public Material[] TrailMaterials;

    private void Awake()
    {
        ballRenderer = GameObject.FindGameObjectWithTag("Top").GetComponent<Renderer>(); // Topun Renderer bileşenine erişmek için GetComponent kullanın
        topTrailRenderer = GameObject.FindGameObjectWithTag("Top").GetComponent<TrailRenderer>(); 
    }

    private void Start()


    {   

        
        string stringBallMat = PlayerPrefs.GetString("LastSelectedBall", gameObject.name);
        string stringTrailMat = PlayerPrefs.GetString("LastSelectedTrail", gameObject.name);

        int numberTrailMat;
        int numberBallMat;

        if (!int.TryParse(stringTrailMat, out numberTrailMat))
        {
            numberTrailMat = 0; // Dönüşüm başarısız olduysa, varsayılan değeri 0 olarak kullan
        }

        if (!int.TryParse(stringBallMat, out numberBallMat))
        {
            numberBallMat = 0; // Dönüşüm başarısız olduysa, varsayılan değeri 0 olarak kullan
        }

        if (PlayerPrefs.GetInt("IsFirstTime", 1) == 1)
        {
            ballRenderer.material=BallMaterials[0];
            topTrailRenderer.material=TrailMaterials[0];

            PlayerPrefs.SetInt("IsFirstTime", 0);
        }
        else 
        {
            ballRenderer.material=BallMaterials[numberBallMat-1];
            topTrailRenderer.material=TrailMaterials[numberTrailMat-1];
        }
        Debug.Log("numberBallMat: " + numberBallMat);
Debug.Log("numberTrailMat: " + numberTrailMat);
        // PlayerPrefs ile kaydedilen platformIndex değerini alın
        int platformIndex = PlayerPrefs.GetInt("SelectedPlatform", 0);

        // platformIndex değerine göre platformu aktifleştirin
        if (platformIndex == 0)
        {
            platform1.SetActive(true);
            platform2.SetActive(false);
            platform3.SetActive(false);
        }
        else if (platformIndex == 1)
        {
            platform1.SetActive(false);
            platform2.SetActive(true);
            platform3.SetActive(false);
        }
        else if (platformIndex == 2)
        {
            platform1.SetActive(false);
            platform2.SetActive(false);
            platform3.SetActive(true);
        }
    }
}
