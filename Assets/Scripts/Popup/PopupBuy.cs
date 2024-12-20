using Sirenix.Utilities;
using UnityEngine.UI;

public class PopupBuy : Popup
{
    BuyGroup[] buyGroups;

    public void Init()
    {
        buyGroups = GetComponentsInChildren<BuyGroup>(true);
        buyGroups.ForEach(x => x.Init());
    }

}