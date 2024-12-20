using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PopupWin : Popup
{
    public void OnNextButtonClick()
    {
        var popup = PopupManager.Instance.ShowPopup<PopupScoreResult>();
        Close();
    }
}