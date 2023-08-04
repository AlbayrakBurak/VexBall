using UnityEngine;

[CreateAssetMenu(fileName = "New BallData", menuName = "Ball Data", order =51)]
public class BallData : ScriptableObject
{
    public Material ballMaterial; // Topun materyalini burada tanımlayın
    public Material trailMaterial; // Trail materyalini burada tanımlayın
    public int currentBallCount;
}
