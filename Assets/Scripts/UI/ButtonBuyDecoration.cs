using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBuyDecoration : MonoBehaviour
{
    public DecorationData data;
    public BuyGroup buyGroup;
    [SerializeField] private Image imgDemo;
    [SerializeField] private TextMeshProUGUI txtCost;
    [SerializeField] private Image imgTick;

    private int index;

    private void Awake()
    {
        index = transform.GetSiblingIndex();
    }

    public void OnClick()
    {
        Debug.Log("Clicked!");
        PlaceId id = buyGroup.placeData.id;
        var unlockData = GameSystem.userdata.dicPlaceUnlock[id];
        bool unlocked = unlockData.boughDecorations.Contains(index);

        if (unlocked)
        {
            //use slot
            unlockData.currentSelected = index;
            GameSystem.userdata.dicPlaceUnlock[id] = unlockData;
            GameSystem.SaveUserDataToLocal();
        } else
        {
            //try buy slot
            buyGroup.BuyDecoration(data, index);
        }
        buyGroup.UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        if (buyGroup == null || buyGroup.placeData == null) return;
        PlaceId id = buyGroup.placeData.id;
        var unlockData = GameSystem.userdata.dicPlaceUnlock[id];

        unlockData.unlocked = unlockData.boughDecorations.Count > 0; //TEST
        GameSystem.SaveUserDataToLocal();

        imgDemo.sprite = data.sprite;
        txtCost.text = data.cost.ToString();
        txtCost.gameObject.SetActive(unlockData.unlocked == false);
        imgTick.gameObject.SetActive(unlockData.unlocked == true && index == unlockData.currentSelected);

        Debug.Log($"place id = {buyGroup.placeData.id.ToString()}, index = {index}, unlockData.unlocked = {unlockData.unlocked}, unlockData.currentSelected = {unlockData.currentSelected}, index == unlockData.currentSelected = {index == unlockData.currentSelected}, imgTick active = {imgTick.gameObject.activeInHierarchy}");
    }
}