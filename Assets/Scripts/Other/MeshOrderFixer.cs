using UnityEngine;

public class MeshOrderFixer : OrderFixer
{
    MeshRenderer meshRenderer;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        meshRenderer.sortingOrder = GetSortingOrder();
    }
}