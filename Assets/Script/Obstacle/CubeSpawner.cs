using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab; // Küp prefabı

    private void Start()
    {   
        //PlayerPrefs.SetInt("Level",33);
        int currentLevel = PlayerPrefs.GetInt("Level");

        int cubeCount = currentLevel / 10;
        if(cubeCount>=10){
            cubeCount=9;
        }
        SpawnCubes(cubeCount);
    }

   private void SpawnCubes(int count)
{
    float totalWidth = count * cubePrefab.transform.localScale.x; // Küplerin toplam genişliği

    float startX = transform.position.x - totalWidth / 2.25f; // Başlangıç x pozisyonu

    

    for (int i = 0; i < count; i++)
    {
        // Küpü oluştur
        GameObject cube = Instantiate(cubePrefab, transform);

        // Küpün x pozisyonunu belirle
        float xPos = startX + i * cubePrefab.transform.localScale.x;
      //  float xRandom = Random.Range(0,0.50f);
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
