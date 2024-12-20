using DG.Tweening;
using UnityEngine.UI;

public class PopupCongratulations : Popup
{
    public override void OnEnable()
    {
        base.OnEnable();
        DOVirtual.DelayedCall(3f, () =>
        {
            PopupManager.Instance.ShowPopup<PopupWin>();
            Close();
        });
    }      
}