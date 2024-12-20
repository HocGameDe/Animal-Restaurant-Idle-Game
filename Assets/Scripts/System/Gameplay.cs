using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public static Gameplay Instance;

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        var popup = PopupManager.Instance.GetPopup<PopupBuy>();
        popup.Init();
    }

    [ContextMenu("Update Sorting Order")]
    public void UpdateSortingOrder()
    {
        var sortingOrders = FindObjectsOfType<SpriteOrderFixer>();
        for (int i = 0; i < sortingOrders.Length; i++)
        {
            sortingOrders[i].UpdateSortingOrder();
        }
    }

    public CandyData GetRandomCandy()
    {
        int rand =  Random.Range(0, 3);
        return DataManager.Instance.dicCandy[(CandyId)rand];
    }
}