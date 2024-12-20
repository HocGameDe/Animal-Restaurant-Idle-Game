using UnityEngine;
using DarkcupGames;
using Newtonsoft.Json;

public class GameSystem : MonoBehaviour
{
    public const string USER_DATA_FILE_NAME = "user_data";
    public static UserData userdata;

    void Awake()
    {
        LoadUserData();
        if (userdata == null)
        {
            userdata = new UserData();
        }
        userdata.CheckValid();
    }

    public static void SaveUserDataToLocal()
    {
        string json = JsonConvert.SerializeObject(GameSystem.userdata);
        string path = FileUtilities.GetWritablePath(GameSystem.USER_DATA_FILE_NAME);
        FileUtilities.SaveFile(System.Text.Encoding.UTF8.GetBytes(json), path, true);
    }

    private void LoadUserData()
    {
        if (!FileUtilities.IsFileExist(GameSystem.USER_DATA_FILE_NAME))
        {
            GameSystem.userdata = new UserData();
            GameSystem.SaveUserDataToLocal();
        }
        else
        {
            GameSystem.userdata = FileUtilities.DeserializeObjectFromFile<UserData>(GameSystem.USER_DATA_FILE_NAME);
            if (userdata == null) userdata = new UserData();
        }
    }
}
