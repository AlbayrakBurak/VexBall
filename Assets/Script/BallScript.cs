using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    public TMP_Text ballCountText;
    public BallData ballData; // BallData ScriptableObject referansı
    public bool isComplete;
    public int maxBallCount = 10;
    public Image ballImage; // Image referansı ekleyin
    public Sprite newImageSprite; // Yeni Sprite referansı

    private Renderer ballRenderer; // Renderer referansı eklendi
    public TrailRenderer topTrailRenderer; // Topun TrailRenderer bileşeni
    public Material[] BallMaterials;
    public Material[] TrailMaterials;

    private void Awake()
    {
        ballRenderer = GameObject.FindGameObjectWithTag("Top").GetComponent<Renderer>(); // Topun Renderer bileşenine erişmek için GetComponent kullanın
        topTrailRenderer = GameObject.FindGameObjectWithTag("Top").GetComponent<TrailRenderer>(); 
        ballImage = GetComponent<Image>(); // Image referansını almak için GetComponent kullanın
    }

    private void Start()
    {
        UpdateBallCountText();
        CheckCompletion(); // Başlangıçta kontrol yapalım
        CheckAndSetDefaultMaterials(); // Önce kayıt var mı kontrol edelim, yoksa default materyalleri ayarlayalım
        LoadData(); // Kaydedilmiş verileri yükle
    }

    private void CheckAndSetDefaultMaterials()
    {
        if (!PlayerPrefs.HasKey("IsFirstTime") || PlayerPrefs.GetInt("IsFirstTime") == 1)
        {
            ballRenderer.material = BallMaterials[0];
            topTrailRenderer.material = TrailMaterials[0];
            
            PlayerPrefs.SetInt("IsFirstTime", 0);
        }
    }

    private void LoadData()
    {
        // BallData nesnesini yükle


        // Topların sayısını yükle
        ballData.currentBallCount = PlayerPrefs.GetInt("CurrentBallCount_" + gameObject.name, 0);

        // Daha önce seçilen topun ve trail'in materyallerini yükle
        string stringBallMat = PlayerPrefs.GetString("LastSelectedBall", "");
        string stringTrailMat = PlayerPrefs.GetString("LastSelectedTrail", "");

        // Varsayılan materyal indeksi
        int defaultBallMatIndex = 0;
        int defaultTrailMatIndex = 0;

        // Eğer PlayerPrefs'te geçersiz veya kayıtlı materyal indeksleri yoksa, varsayılan materyal indekslerini kullan
        int numberBallMat = 0;
        int numberTrailMat = 0;

        if (!string.IsNullOrEmpty(stringBallMat) && int.TryParse(stringBallMat, out numberBallMat) && numberBallMat > 0 && numberBallMat <= BallMaterials.Length)
            ballRenderer.material = BallMaterials[numberBallMat - 1];
        else
            ballRenderer.material = BallMaterials[defaultBallMatIndex];

        if (!string.IsNullOrEmpty(stringTrailMat) && int.TryParse(stringTrailMat, out numberTrailMat) && numberTrailMat > 0 && numberTrailMat <= TrailMaterials.Length)
            topTrailRenderer.material = TrailMaterials[numberTrailMat - 1];
        else
            topTrailRenderer.material = TrailMaterials[defaultTrailMatIndex];
    }

    private void UpdateBallCountText()
    {
        ballCountText.text = ballData.currentBallCount + "/" + maxBallCount;
    }

    private void CheckCompletion()
    {
        // Toplar 10/10 olduğunda texti kapat ve Image'i değiştir
        if (ballData.currentBallCount >= maxBallCount && !isComplete)
        {
            ballCountText.enabled = false;
            ChangeImage(); // 10/10 olduğunda Image'i değiştir
            isComplete = true;
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
        else if (isComplete && ballData.currentBallCount >= maxBallCount)
        {
            ballRenderer.material = ballData.ballMaterial;
            topTrailRenderer.material = ballData.trailMaterial;
             PlayerPrefs.SetString("LastSelectedBall", gameObject.name);
         PlayerPrefs.SetString("LastSelectedTrail", gameObject.name);
        }

        
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("CurrentBallCount_" + gameObject.name, ballData.currentBallCount);
    }
}
