using UnityEngine;

public class ButtonClose : MonoBehaviour
{
    private Popup popup;

    private void Awake()
    {
        if (transform.parent != null)
        {
            popup = transform.parent.parent?.GetComponentInChildren<Popup>();
        }
        else
        {
            popup = transform.parent?.GetComponentInChildren<Popup>();
        }
    }

    public void OnClick()
    {
        popup?.Close();
    }
}