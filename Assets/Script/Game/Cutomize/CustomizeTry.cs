using UnityEngine;

public class CustomizeTry : MonoBehaviour
{
    public GameObject top; // Top objesini referans alır
    public Material[] materials; // Farklı materyalleri tutan bir dizi

    private Renderer topRenderer; // Top objesinin Renderer bileşeni

    private void Start()
    {
        topRenderer = top.GetComponent<Renderer>();
    }

    // Butonlara tıklanma durumunu kontrol etmek için çağrılır
    public void OnButtonClicked(int materialIndex)
    {
        if (materialIndex < materials.Length)
        {
            // Tıklanan butonun üzerindeki materyali topa uygular
            topRenderer.material = materials[materialIndex];
        }
    }
}
