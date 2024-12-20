using UnityEngine;
using System.Collections.Generic;
using DarkcupGames;
using System.Linq;

public class RestaurantManager : MonoBehaviour
{
    public static RestaurantManager Instance;
    public List<Table> tables;
    private void Awake()
    {
        Instance = this;
        tables = FindObjectsOfType<Table>().ToList();
    }
    public Table FindEmptyTable()
    {
        tables.Shuffle();
        for (int i = 0; i < tables.Count; i++)
        {
            if (tables[i].visitor == null) return tables[i];
        }
        return null;
    }
}