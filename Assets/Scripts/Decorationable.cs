using UnityEngine;

public class Decorationable : MonoBehaviour
{
    public DecorationData data;
    [SerializeField] private SpriteRenderer visual;

    [ContextMenu("Update Decoration")]
    public void UpdateDecoration()
    {
        visual.sprite = data.sprite;
    }
}