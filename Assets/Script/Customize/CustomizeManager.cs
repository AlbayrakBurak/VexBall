using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeManager : MonoBehaviour
{   
        public GameObject platform1; // İlk platform objesi
    public GameObject platform2; // İkinci platform objesi
    public GameObject platform3; // Üçüncü platform objesi
    // Start is called before the first frame update
    void Start()
    {
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