using UnityEngine;
using UnityEngine.UI;

public class CustomizeScene : MonoBehaviour
{
    public GameObject topObject; // Top objesi
    public Renderer topRenderer; // Top objesinin Renderer bileşeni
    public Material[] topMaterials; // Top materyalleri
    public TrailRenderer topTrailRenderer; // Topun TrailRenderer bileşeni
    public GameObject[] platformObjects; // Platform objelerini tutacak dizi
    public Button platformRightButton; // Platform sağ ok butonu
    public Button platformLeftButton; // Platform sol ok butonu
    public Button topRightButton; // Top sağ ok butonu
    public Button topLeftButton; // Top sol ok butonu

    private int platformIndex = 0; // Seçili platform indeksi
    private int materialIndex = 0; // Seçili top materyal indeksi

    private void Start()
    {
        // Başlangıçta seçili olan platformu etkinleştir
        UpdatePlatformSelection();

        // Buton tıklama olaylarını atama
        platformRightButton.onClick.AddListener(SelectNextPlatform);
        platformLeftButton.onClick.AddListener(SelectPreviousPlatform);
        topRightButton.onClick.AddListener(SelectNextTopMaterial);
        topLeftButton.onClick.AddListener(SelectPreviousTopMaterial);
    }

    // Sonraki platformu seçen metod
    private void SelectNextPlatform()
    {
        // Platform indeksini artır
        platformIndex++;
        if (platformIndex >= platformObjects.Length)
        {
            platformIndex = 0; // Dizinin sonuna gelindiğinde başa dön
        }

        // Etkinleştirilecek platformu güncelle
        UpdatePlatformSelection();
    }

    // Önceki platformu seçen metod
    private void SelectPreviousPlatform()
    {
        // Platform indeksini azalt
        platformIndex--;
        if (platformIndex < 0)
        {
            platformIndex = platformObjects.Length - 1; // Dizinin başına gelindiğinde sona git
        }

        // Etkinleştirilecek platformu güncelle
        UpdatePlatformSelection();
    }

    // Platform seçimini güncelleyen metod
    private void UpdatePlatformSelection()
    {
        // Tüm platform objelerini devre dışı bırak
        for (int i = 0; i < platformObjects.Length; i++)
        {
            platformObjects[i].SetActive(false);
        }

        // Seçilen platformu etkinleştir
        platformObjects[platformIndex].SetActive(true);
    }

    // Sonraki top materyalini seçen metod
    private void SelectNextTopMaterial()
    {
        // Materyal indeksini artır
        materialIndex++;
        if (materialIndex >= topMaterials.Length)
        {
            materialIndex = 0; // Dizinin sonuna gelindiğinde başa dön
        }

        // Seçili top materyalini güncelle
        UpdateTopMaterial();
    }

    // Önceki top materyalini seçen metod
    private void SelectPreviousTopMaterial()
    {
        // Materyal indeksini azalt
        materialIndex--;
        if (materialIndex < 0)
        {
            materialIndex = topMaterials.Length - 1; // Dizinin başına gelindiğinde sona git
        }

        // Seçili top materyalini güncelle
        UpdateTopMaterial();
    }

    // Top materyalini güncelleyen metod
    private void UpdateTopMaterial()
    {
        // Seçili top materyalini uygula
        topRenderer.material = topMaterials[materialIndex];

        // Seçili top materyalini TrailRenderer bileşenine de uygula
        if (topTrailRenderer != null)
        {
            topTrailRenderer.material = topMaterials[materialIndex];
        }
    }
}
