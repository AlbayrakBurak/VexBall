using UnityEngine;

public class CustomizeGameManager : MonoBehaviour
{
    public BallData ballData; // BallData ScriptableObject referansı

    private void Start()
    {
        LoadData(); // Oyun başladığında verileri yükle
    }

    private void OnApplicationQuit()
    {
        SaveData(); // Oyun kapatıldığında verileri kaydet
    }

    private void SaveData()
    {
        // Hiçbir şey yapmamız gerekmeyecek çünkü ScriptableObject otomatik olarak verileri saklayacak.
    }

    private void LoadData()
    {
        // Hiçbir şey yapmamız gerekmeyecek çünkü ScriptableObject otomatik olarak verileri yükleyecek.
    }
}
