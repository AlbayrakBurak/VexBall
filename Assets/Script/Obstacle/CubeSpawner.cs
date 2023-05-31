using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab; // Küp prefabı
    public float minPosX = -1.75f; // Küplerin minimum x pozisyonu
    public float maxPosX = 1.75f; // Küplerin maksimum x pozisyonu
    public float spacing = 1.5f; // Küpler arasındaki uzaklık

    private void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("Level");
        int cubeCount = currentLevel / 10;

        SpawnCubes(cubeCount);
    }

    private void SpawnCubes(int count)
    {
        float totalWidth = (count - 1) * spacing; // Küplerin toplam genişliği

        float startX = transform.position.x - totalWidth / 2f; // Başlangıç x pozisyonu

        for (int i = 0; i < count; i++)
        {
            // Küpü oluştur
            GameObject cube = Instantiate(cubePrefab, transform);

            // Küpün x pozisyonunu belirle
            float xPos = startX + i * spacing;
            xPos += Random.Range(minPosX, maxPosX); // Rastgele offset uygula

            // Çakışma kontrolü yap
            bool isColliding = CheckCollision(cube, xPos);
            int collisionAttempts = 0;
            while (isColliding && collisionAttempts < 10)
            {
                xPos = startX + i * spacing; // Başlangıç x pozisyonuna geri dön
                xPos += Random.Range(minPosX, maxPosX); // Farklı bir konum belirle
                isColliding = CheckCollision(cube, xPos);
                collisionAttempts++;
            }

            cube.transform.position = new Vector3(xPos, 0f, 0f);
        }
    }

    private bool CheckCollision(GameObject cube, float xPos)
    {
        Collider[] colliders = Physics.OverlapBox(new Vector3(xPos, 0f, 0f), cube.transform.localScale / 2f);
        foreach (Collider collider in colliders)
        {
            if (collider != cube.GetComponent<Collider>())
            {
                return true; // Çakışma var
            }
        }
        return false; // Çakışma yok
    }
}
