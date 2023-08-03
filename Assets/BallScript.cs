
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    public TMP_Text ballCountText;
    public BallData ballData; // BallData ScriptableObject referansı
    public bool isComplete;
    private int maxBallCount = 10;
    public Image ballImage; // Image referansı ekleyin
    public Sprite newImageSprite; // Yeni Sprite referansı

    public Material BallSkin;
    public Material TrailSkin;

    private Renderer ballRenderer; // Renderer referansı eklendi
    private Material originalMaterial; // Orijinal malzeme referansı eklendi
    public TrailRenderer topTrailRenderer; // Topun TrailRenderer bileşeni

    private void Start()
    {
       ballRenderer = GameObject.FindGameObjectWithTag("Top").GetComponent<Renderer>(); // Topun Renderer bileşenine erişmek için GetComponent kullanın
        topTrailRenderer=GameObject.FindGameObjectWithTag("Top").GetComponent<TrailRenderer>(); 
        ballImage = GetComponent<Image>(); // Image referansını almak için GetComponent kullanın
        originalMaterial = ballRenderer.material; // Orijinal malzemeyi kaydedin
        LoadData(); // Kaydedilen verileri yükle
        UpdateBallCountText();
        CheckCompletion(); // Başlangıçta kontrol yapalım
    }

    private void UpdateBallCountText()
    {
        ballCountText.text = ballData.currentBallCount + "/" + maxBallCount;
    }

    private void CheckCompletion()
    {
        // Toplar 10/10 olduğunda texti kapat ve Image'i değiştir
        if (ballData.currentBallCount >= maxBallCount)
        {
            if (!isComplete)
            {
                ballCountText.enabled = false;
                ChangeImage(); // 10/10 olduğunda Image'i değiştir
                ballRenderer.material = BallSkin; // Topun materyalini değiştir
                topTrailRenderer.material = TrailSkin;
                isComplete = true;
            }
            else
            {
                //  ballRenderer.material = BallSkin; // Son tıklanan topun materyalini yükle
                //  topTrailRenderer.material = TrailSkin; // Son tıklanan topun trail materyalini yükle
            }
        }
    }

    private void ChangeImage()
    {
        ballImage.sprite = newImageSprite; // Yeni Sprite'i Image'e atayın
    }

    public void OnBallButtonClick()
    {
        if (ballData.currentBallCount < maxBallCount)
        {
            ballData.currentBallCount++;
            UpdateBallCountText();
            SaveData(); // Top sayısını kaydet
            CheckCompletion(); // Her tıklamada kontrol yapalım
        }
        else if (isComplete && ballData.currentBallCount >= maxBallCount){
            ballRenderer.material = BallSkin;
            topTrailRenderer.material=TrailSkin;



        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("CurrentBallCount_" + gameObject.name, ballData.currentBallCount);
        // Top'un materyalini PlayerPrefs ile kaydet
        PlayerPrefs.SetString("BallMaterial_" + gameObject.name, BallSkin.name);
        // Top'un trail materyalini PlayerPrefs ile kaydet
        PlayerPrefs.SetString("TrailMaterial_" + gameObject.name, TrailSkin.name);
    }

    private void LoadData()
    {
        ballData.currentBallCount = PlayerPrefs.GetInt("CurrentBallCount_" + gameObject.name, 0);
        UpdateBallCountText(); // Yüklenen değeri güncellemeyi unutmayın

        // Top'un materyalini PlayerPrefs'tan yükleyip uygula
        string savedBallMaterialName = PlayerPrefs.GetString("BallMaterial_" + gameObject.name, "");
        if (!string.IsNullOrEmpty(savedBallMaterialName))
        {
            Material savedBallMaterial = Resources.Load<Material>(savedBallMaterialName);
            if (savedBallMaterial != null)
            {
                BallSkin = savedBallMaterial;
                ballRenderer.material = BallSkin;
            }
        }

        // Top'un trail materyalini PlayerPrefs'tan yükleyip uygula
        string savedTrailMaterialName = PlayerPrefs.GetString("TrailMaterial_" + gameObject.name, "");
        if (!string.IsNullOrEmpty(savedTrailMaterialName))
        {
            Material savedTrailMaterial = Resources.Load<Material>(savedTrailMaterialName);
            if (savedTrailMaterial != null)
            {
                TrailSkin = savedTrailMaterial;
                topTrailRenderer.material = TrailSkin;
            }
        }
    }
}