using TMPro;
using UnityEngine.UI;

public class PopupNotEnoughMoney : Popup
{
    public TextMeshProUGUI txtMoney;

    public void Show(int requireMoney)
    {
        txtMoney.text = requireMoney.ToString();
    }
}