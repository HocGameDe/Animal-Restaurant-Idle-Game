using UnityEngine;

public class OrderFixer : MonoBehaviour
{
    public int offset;
    public int GetSortingOrder()
    {
        return -(int)(transform.position.y * 100) + offset;
    }
}