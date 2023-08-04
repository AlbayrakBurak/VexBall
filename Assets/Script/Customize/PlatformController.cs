using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public int platformIndex; // Platformların benzersiz indeksleri

    private void Start()
    {
        // PlayerPrefs ile kaydedilen platform indeksini yükleyin
        int savedPlatformIndex = PlayerPrefs.GetInt("SelectedPlatform", 0);
        // Eğer daha önce seçilen bir platform varsa ve bu platform şu an aktifse,
        // etkinleştirin.
        if (savedPlatformIndex == platformIndex && !gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

    // Butona tıklandığında çağrılacak fonksiyon
    public void OnButtonClick()
    {
        // Etkinleştirilen platformun indeksini PlayerPrefs'e kaydedin
        PlayerPrefs.SetInt("SelectedPlatform", platformIndex);

        // Tüm platformları devre dışı bırakın ve sadece tıklanan platformu etkinleştirin
        foreach (PlatformController platform in FindObjectsOfType<PlatformController>())
        {
            platform.gameObject.SetActive(false);
        }

        gameObject.SetActive(true);
    }
}
