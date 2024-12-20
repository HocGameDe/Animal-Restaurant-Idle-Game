using UnityEngine;

[CreateAssetMenu]
public class CandyData : ScriptableObject
{
    public CandyId id;
    public string candyName;
    public Sprite sprite;
    public float rewardValue;
    public int star;
}