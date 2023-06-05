using UnityEngine;

public class CubeHealth : MonoBehaviour
{
    public int maxHealth = 5; // Maksimum can miktarı
    public int currentHealth; // Mevcut can miktarı
    public Material health5; // Kırmızı malzeme
    public Material health4; // Mavi malzeme
    public Material health3; // Yeşil malzeme
    public Material health2; // Mor malzeme
    public Material health1; // Beyaz malzeme
    [SerializeField] private float UygulanacakGuc;

    private Renderer cubeRenderer; // Küpün Renderer bileşeni

    private void Start()
    {   
        currentHealth = maxHealth; // Başlangıçta mevcut canı maksimum cana ayarla
        cubeRenderer = GetComponent<Renderer>(); // Küpün Renderer bileşenini al
        UpdateCubeColor(); // Küp rengini güncelle

        // Örnek bir level değeriyle UpdateCurrentHealth() fonksiyonunu çağırabilirsiniz
        int level = PlayerPrefs.GetInt("Level");
        UpdateCurrentHealth(level);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Top"))
        {
            TakeDamage(); // Top çarptığında hasar al
            UpdateCubeColor(); // Küp rengini güncelle
            Vector3 collisionPoint = collision.contacts[0].point;
            Vector3 forceDirection = collisionPoint - transform.position;
            forceDirection.Normalize();
            collision.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * UygulanacakGuc, ForceMode.Force);
        }
    }

    private void TakeDamage()
    {
        currentHealth--; // Can miktarını azalt

        if (currentHealth <= 0)
        {
            Destroy(gameObject); // Can sıfırlanmışsa küpü yok et
        }
    }

    private void UpdateCubeColor()
    {
        // Can miktarına göre küp rengini güncelle
        if (currentHealth >= 5)
        {
            cubeRenderer.material = health5;
        }
        else if (currentHealth == 4)
        {
            cubeRenderer.material = health4;
        }
        else if (currentHealth == 3)
        {
            cubeRenderer.material = health3;
        }
        else if (currentHealth == 2)
        {
            cubeRenderer.material = health2;
        }
        else
        {
            cubeRenderer.material = health1;
        }
    }

    private void UpdateCurrentHealth(int level)
    {
        if (level >= 50)
        {
            currentHealth = 5;
        }
        else if (level >= 40)
        {
            currentHealth = 4;
        }
        else if (level >= 30)
        {
            currentHealth = 3;
        }
        else if (level >= 20)
        {
            currentHealth = 2;
        }
        else if (level >= 10)
        {
            currentHealth = 1;
        }
        else
        {
            currentHealth = maxHealth;
        }

        UpdateCubeColor(); // Küp rengini güncelle
    }
}

//DİĞER İKİ PLATFORM KONTOL