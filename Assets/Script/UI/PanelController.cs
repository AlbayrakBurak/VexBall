using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelController : MonoBehaviour
{
    public GameObject upperPanel; // Üst panelin referansı
    public GameObject BallPanel; // İlk panelin referansı
    public GameObject PlatformPanel; // İkinci panelin referansı
    public GameObject NoAds;

    // Diğer değişkenleriniz aynı

    private void Start()
    {

        int noAdsValue = PlayerPrefs.GetInt("NoAds", 0);

            if (noAdsValue != 1)
            {
                NoAds.SetActive(true);
            }
             else{
                
            NoAds.SetActive(false);
        }

         upperPanel.SetActive(true);
        BallPanel.SetActive(true);
        PlatformPanel.SetActive(false);

    }

    // Diğer fonksiyonlarınız aynı

    // İlk butona tıklandığında çağrılacak fonksiyon
    public void OpenBallPanel()
    {
       
        BallPanel.SetActive(true);
        PlatformPanel.SetActive(false);
    }

    // İkinci butona tıklandığında çağrılacak fonksiyon
    public void OpenPlatformPanel()
    {
        BallPanel.SetActive(false);
        PlatformPanel.SetActive(true);
    }

    // Geri butonuna tıklandığında çağrılacak fonksiyon
    public void BuyNoAds()
    {
       
    }

    public void BackToMenu(){
        SceneManager.LoadScene("MainScene");
    }
}
