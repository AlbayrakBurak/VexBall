using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject upperPanel; // Üst panelin referansı
    public GameObject BallPanel; // İlk panelin referansı
    public GameObject PlatformPanel; // İkinci panelin referansı

    private void Start()
    {
        // Başlangıçta sadece üst panel görünür olacak
        upperPanel.SetActive(true);
        BallPanel.SetActive(false);
        PlatformPanel.SetActive(false);
    }

    // İlk butona tıklandığında çağrılacak fonksiyon
    public void BallButtonClicked()
    {
        upperPanel.SetActive(false);
        BallPanel.SetActive(true);
        PlatformPanel.SetActive(false);
    }

    // İkinci butona tıklandığında çağrılacak fonksiyon
    public void PlatformButtonClicked()
    {
        upperPanel.SetActive(false);
        BallPanel.SetActive(false);
        PlatformPanel.SetActive(true);
    }

    // Geri butonuna tıklandığında çağrılacak fonksiyon
    public void OnBackButtonClicked()
    {
        upperPanel.SetActive(true);
        BallPanel.SetActive(false);
        PlatformPanel.SetActive(false);
    }
}
