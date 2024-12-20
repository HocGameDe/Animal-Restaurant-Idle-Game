using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SerializedMonoBehaviour
{
    public static DataManager Instance;
    [SerializeField] List<PlaceData> places;
    [SerializeField] List<CandyData> candies;

    public Dictionary<PlaceId, PlaceData> dicPlace;
    public Dictionary<CandyId, CandyData> dicCandy;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Init()
    {
        dicPlace = new Dictionary<PlaceId, PlaceData>();
        for (int i = 0; i < places.Count; i++)
        {
            PlaceData placeData = places[i];
            if (!dicPlace.ContainsKey(placeData.id))
            {
                dicPlace.Add(placeData.id, placeData);
            }
            else
            {
                Debug.LogWarning($"Duplicate PlaceId found: {placeData.id}. Entry ignored.");
            }
        }

        dicCandy = new Dictionary<CandyId, CandyData>();
        for (int i = 0; i < candies.Count; i++)
        {
            CandyData candyData = candies[i];
            if (!dicCandy.ContainsKey(candyData.id))
            {
                dicCandy.Add(candyData.id, candyData);
            }
            else
            {
                Debug.LogWarning($"Duplicate CandyId found: {candyData.id}. Entry ignored.");
            }
        }
    }
}