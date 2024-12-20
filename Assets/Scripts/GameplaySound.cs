using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public enum GameplaySoundId
{
    BackgroundSong,
    WinSound,
    LoseSound,
    CollectSmall,
    CollectBig,
    FlyToGoalSound,
    MoneySound,
    MainMenuSound,
    LevelupSound,
    GoalCompleteSound,
    StarAppearSound,
    ButtonSound
}

public class GameplaySound : MonoBehaviour
{
    public static GameplaySound Instance;

    public AssetReference collectSmallSound;
    public AssetReference collectBigSound;
    public AssetReference winSound;
    public AssetReference loseSound;
    public AssetReference backgroundSong;
    public AssetReference flyToGoalSound;
    public AssetReference moneySound;
    public AssetReference mainMenuSound;
    public AssetReference levelupSound;
    public AssetReference goalCompleteSound;
    public AssetReference starAppearSound;
    public AssetReference buttonSound;

    public AudioSource[] audioSources;
    public AudioSource backgroundSongSource;

    public Dictionary<GameplaySoundId, AssetReference> soundMap;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            soundMap = new Dictionary<GameplaySoundId, AssetReference>
            {
                { GameplaySoundId.CollectSmall, collectSmallSound },
                { GameplaySoundId.CollectBig, collectBigSound },
                { GameplaySoundId.WinSound, winSound },
                { GameplaySoundId.LoseSound, loseSound },
                { GameplaySoundId.BackgroundSong, backgroundSong },
                { GameplaySoundId.FlyToGoalSound, flyToGoalSound },
                { GameplaySoundId.MoneySound, moneySound },
                { GameplaySoundId.MainMenuSound, mainMenuSound },
                { GameplaySoundId.LevelupSound, levelupSound },
                { GameplaySoundId.GoalCompleteSound, goalCompleteSound },
                { GameplaySoundId.StarAppearSound, starAppearSound },
                { GameplaySoundId.ButtonSound, buttonSound }
            };
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (GameSystem.userdata.dicSetting[SettingKey.Music] == false && backgroundSongSource.isPlaying)
        {
            backgroundSongSource.Stop();
        }
        if (GameSystem.userdata.dicSetting[SettingKey.Music] == true && backgroundSongSource.isPlaying == false)
        {
            backgroundSongSource.Play();
        }
    }

    public void PlaySoundAddressable(GameplaySoundId soundId, float volumne = 1f)
    {
        if (GameSystem.userdata.dicSetting[SettingKey.Sound] == false) return;
        if (soundMap.TryGetValue(soundId, out AssetReference assetReference))
        {
            if (assetReference.IsValid())
            {
                PlaySound((AudioClip)assetReference.Asset, volumne);
                return;
            }
            var handle = assetReference.LoadAssetAsync<AudioClip>();
            handle.Completed += (result) =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    PlaySound(result.Result, volumne);
                }
                else
                {
                    Debug.LogError(result.OperationException);
                }
            };
        }
    }

    public void PlayBackgroundSongAddressable(GameplaySoundId soundId)
    {
        if (soundMap.TryGetValue(soundId, out AssetReference assetReference))
        {
            if (assetReference.IsValid())
            {
                PlayBackgroundSound((AudioClip)assetReference.Asset);
                return;
            }
            var handle = assetReference.LoadAssetAsync<AudioClip>();
            handle.Completed += (result) =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    PlayBackgroundSound(result.Result);
                }
                else
                {
                    Debug.LogError(result.OperationException);
                }
            };
        }
    }

    private void PlaySound(AudioClip clip, float volumne = 1f)
    {
        foreach (var audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clip;
                audioSource.volume = volumne;
                audioSource.Play();
                break;
            }
        }
    }

    private void PlayBackgroundSound(AudioClip clip)
    {
        if (backgroundSongSource.isPlaying == false || backgroundSongSource.clip != clip)
        {
            backgroundSongSource.clip = clip;
            backgroundSongSource.Play();
        }
    }
}