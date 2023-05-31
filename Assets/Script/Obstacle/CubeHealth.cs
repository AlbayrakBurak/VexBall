using UnityEngine;

public class CubeHealth : MonoBehaviour
{
    public int maxHealth = 5; // Maksimum can miktarı
    public int currentHealth; // Mevcut can miktarı
    public Material redMaterial; // Kırmızı malzeme
    public Material blueMaterial; // Mavi malzeme
    public Material greenMaterial; // Yeşil malzeme
    public Material purpleMaterial; // Mor malzeme
    public Material whiteMaterial; // Beyaz malzeme
    [SerializeField] private float UygulanacakGuc;

    private Renderer cubeRenderer; // Küpün Renderer bileşeni

    private void Start()
    {
        currentHealth = maxHealth; // Başlangıçta mevcut canı maksimum cana ayarla
        cubeRenderer = GetComponent<Renderer>(); // Küpün Renderer bileşenini al
        UpdateCubeColor(); // Küp rengini güncelle
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
            cubeRenderer.material = redMaterial;
        }
        else if (currentHealth == 4)
        {
            cubeRenderer.material = purpleMaterial;
        }
        else if (currentHealth == 3)
        {
            cubeRenderer.material = greenMaterial;
        }
        else if (currentHealth == 2)
        {
            cubeRenderer.material = blueMaterial;
        }
        else
        {
            cubeRenderer.material = whiteMaterial;
        }
    }
}
