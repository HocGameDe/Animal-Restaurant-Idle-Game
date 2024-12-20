using TMPro;
using UnityEngine;

public abstract class TextUpdater : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public float currentValue = -1;

    private void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (GetValue() != currentValue)
        {
            currentValue = GetValue();
            UpdateText(currentValue);
        }
    }
    public abstract float GetValue();
    public virtual void UpdateText(float newValue)
    {
        txt.text = newValue.ToString("F0");
    }
}