using System;
using System.Collections.Generic;
using UnityEngine;

//public enum PopupId 
//{ 
//    PopupWin, PopupGameOver, PopupSetting, PopupPause, PopupRetry , PopupTarget
//}

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;
    public Canvas canvasPopup;
    private Dictionary<Type, Popup> dicPopup;

    private void Awake()
    {
        Instance = this;
        dicPopup = new Dictionary<Type, Popup>();
        var popups = canvasPopup.GetComponentsInChildren<Popup>(true);

        foreach (Popup popup in popups)
        {
            if (!dicPopup.ContainsKey(popup.GetType()))
            {
                dicPopup.Add(popup.GetType(), popup);
            }
            else
            {
                Debug.LogError($"Duplicate PopupId {popup.GetType()} found in PopupManager.");
            }
        }
    }

    public T ShowPopup<T>() where T : Popup
    {
        if (dicPopup.TryGetValue(typeof(T), out Popup popup))
        {
            popup.gameObject.SetActive(true);
            popup.transform.SetAsLastSibling();
            return (T)popup;
        }
        else
        {
            Debug.LogError($"Popup with id {typeof(T)} not found in PopupManager.");
            return default;
        }
    }

    public T HidePopup<T>() where T:Popup
    {
        if (dicPopup.TryGetValue(typeof(T), out Popup popup))
        {
            popup.gameObject.SetActive(false);
            return (T)popup;
        }
        else
        {
            Debug.LogError($"Popup with id {typeof(T)} not found in PopupManager.");
            return default;
        }
    }

    public T GetPopup<T>() where T : Popup
    {
        if (dicPopup.TryGetValue(typeof(T), out Popup popup))
        {
            return (T)popup;
        }
        return default;
    }
}