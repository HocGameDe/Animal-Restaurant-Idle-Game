using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOrderFixer : OrderFixer
{
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        spriteRenderer.sortingOrder = GetSortingOrder();
    }

    public void UpdateSortingOrder()
    {
        GetComponent<SpriteRenderer>().sortingOrder = GetSortingOrder();
    }
}