using TMPro;
using UnityEngine;

public class BuyGroup : MonoBehaviour
{
    public PlaceData placeData;
    public Decorationable decorationable;
    public int selectedIndex;

    [SerializeField] private TextMeshProUGUI txtPlaceName;
    private ButtonBuyDecoration[] buttons;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        buttons = GetComponentsInChildren<ButtonBuyDecoration>();
        txtPlaceName.text = placeData.placeName;
        for (int i = 0; i < placeData.decorations.Count; i++)
        {
            if (i >= buttons.Length)
            {
                Debug.LogError("not enough button for showing data");
                return;
            }
            buttons[i].buyGroup = this;
            buttons[i].data = placeData.decorations[i];
            buttons[i].UpdateDisplay();
        }
        for (int i = placeData.decorations.Count; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        UpdateDisplay();
    }

    public void BuyDecoration(DecorationData data, int index)
    {
        int cost = data.cost;
        if (GameSystem.userdata.gold < cost)
        {
            var popup = PopupManager.Instance.ShowPopup<PopupNotEnoughMoney>();
            popup.Show(cost);
            return;
        }

        var unlockData = GameSystem.userdata.dicPlaceUnlock[placeData.id];
        unlockData.unlocked = true;
        if (unlockData.boughDecorations.Contains(index) == false) unlockData.boughDecorations.Add(index);
        unlockData.currentSelected = index;

        GameSystem.userdata.dicPlaceUnlock[placeData.id] = unlockData;
        GameSystem.userdata.gold -= cost;
        GameSystem.SaveUserDataToLocal();

        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        int selectedIndex = 0;
        if (GameSystem.userdata.dicPlaceUnlock[placeData.id].currentSelected >= 0)
        {
            selectedIndex = GameSystem.userdata.dicPlaceUnlock[placeData.id].currentSelected;
        }
        decorationable.data = placeData.decorations[selectedIndex];
        decorationable.UpdateDecoration();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].UpdateDisplay();
        }
    }
}