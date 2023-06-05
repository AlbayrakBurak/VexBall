using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    public Collider leftWall;   // Sol sınır collider'ı
    public Collider rightWall;  // Sağ sınır collider'ı
    public Collider topWall;    // Üst sınır collider'ı
    public Collider bottomWall; // Alt sınır collider'ı

    void Start()
    {
        SetScreenBounds();
    }

    void SetScreenBounds()
    {
        Camera mainCamera = Camera.main;

        float distance = Mathf.Abs(mainCamera.transform.position.z);

        Vector3 leftBounds = mainCamera.ScreenToWorldPoint(new Vector3(0f, 0f, distance));
        Vector3 rightBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, distance));
        Vector3 topBounds = mainCamera.ScreenToWorldPoint(new Vector3(0f, Screen.height, distance));
        Vector3 bottomBounds = mainCamera.ScreenToWorldPoint(new Vector3(0f, 0f, distance));

        leftWall.transform.position = new Vector3(leftBounds.x-.40f, leftBounds.y, 0f);
        rightWall.transform.position = new Vector3(rightBounds.x+.40f, rightBounds.y, 0f);
        topWall.transform.position = new Vector3(topBounds.x, topBounds.y-1f, 0f);
        bottomWall.transform.position = new Vector3(bottomBounds.x, bottomBounds.y, 0f);
    }
}
