using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SettingKey
{
    Sound, Music, Vibration
}

public enum LevelState
{
    Locked, Finish, Playing
}

[System.Serializable]
public class PlaceUnlockData
{
    public bool unlocked = false;
    public int currentSelected = -1;
    public List<int> boughDecorations = new List<int>();
}

[System.Serializable]
public class UserData
{
    public const int DEFAULT_POWERUP_AMOUNT = 3;

    public int level;
    public int maxLevel;
    public float gold;
    public float diamond;
    public Dictionary<string, LevelState> levelLib = new Dictionary<string, LevelState>();
    public Dictionary<SettingKey, bool> dicSetting = new Dictionary<SettingKey, bool>();
    public Dictionary<PlaceId, PlaceUnlockData> dicPlaceUnlock = new Dictionary<PlaceId, PlaceUnlockData>();
    public List<string> boughtItems;

    public UserData()
    {
        boughtItems = new List<string>();
        levelLib = new Dictionary<string, LevelState>();
    }
    public void CheckValid()
    {
        if (boughtItems == null) boughtItems = new List<string>();
        if (dicSetting == null) dicSetting = new Dictionary<SettingKey, bool>();
        if (dicSetting.ContainsKey(SettingKey.Sound) == false) dicSetting.Add(SettingKey.Sound, true);
        if (dicSetting.ContainsKey(SettingKey.Music) == false) dicSetting.Add(SettingKey.Music, true);
        if (dicSetting.ContainsKey(SettingKey.Vibration) == false) dicSetting.Add(SettingKey.Vibration, true);
        if (dicPlaceUnlock == null) dicPlaceUnlock = new Dictionary<PlaceId, PlaceUnlockData>();
        var values = Enum.GetValues(typeof(PlaceId));
        foreach (PlaceId placeId in values)
        {
            if (dicPlaceUnlock.ContainsKey(placeId) == false)
            {
                dicPlaceUnlock.Add(placeId, new PlaceUnlockData()); ;
            }
        }
    }
}