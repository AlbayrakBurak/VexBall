// BallData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "New BallData", menuName = "Ball Data", order = 51)]
public class BallData : ScriptableObject
{
    public Material ballMaterial; // Topun malzemesini burada tanımlayın
    public Material trailMaterial; // Trail malzemesini burada tanımlayın
    public int currentBallCount;
}
