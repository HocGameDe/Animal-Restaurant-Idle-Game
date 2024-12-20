using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlaceData : ScriptableObject
{
    public PlaceId id;
    public string placeName;
    public Sprite sprite;
    public List<DecorationData> decorations;
}
