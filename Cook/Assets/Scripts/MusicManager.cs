using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClipSO clipSO;

    // 添加背景音乐 AudioSource
    [SerializeField] private AudioSource backgroundMusicSource;

    public static MusicManager Instance { get; private set; }

    // 音量设置
    private float musicVolume = 0.8f;
    private float sfxVolume = 0.8f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // 确保在场景切换时不销毁
        // 先确保它是根对象
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);

        // 初始化音量设置
        LoadVolumeSettings();

        // 设置背景音乐音量
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.volume = musicVolume;
            if (!backgroundMusicSource.isPlaying)
            {
                backgroundMusicSource.Play();
            }
        }
    }

    private void LoadVolumeSettings()
    {
        // 从PlayerPrefs加载音量设置
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            int musicValue = PlayerPrefs.GetInt("MusicVolume");
            musicVolume = musicValue / 10f;
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            int sfxValue = PlayerPrefs.GetInt("SFXVolume");
            sfxVolume = sfxValue / 10f;
        }
    }

    private void PlaySound(AudioClip[] clips, Vector3 position, float volume = 1.0f)
    {
        if (clips == null || clips.Length == 0)
        {
            Debug.LogWarning("No audio clips available!");
            return;
        }

        int index = Random.Range(0, clips.Length);
        float finalVolume = volume * sfxVolume;

        AudioSource.PlayClipAtPoint(clips[index], position, finalVolume);
    }

    // =========== 公共音效播放方法 ===========

    // 切割音效
    public void PlayChopSound(Vector3 position = default, float volume = 1.0f)
        => PlaySound(clipSO.chop, position, volume);

    // 失败音效
    public void PlayFailSound(Vector3 position = default, float volume = 1.0f)
        => PlaySound(clipSO.fail, position, volume);

    // 成功音效
    public void PlaySuccessSound(Vector3 position = default, float volume = 1.0f)
        => PlaySound(clipSO.success, position, volume);

    // 脚步声
    public void PlayFootstepSound(Vector3 position = default, float volume = 1.0f)
        => PlaySound(clipSO.footstep, position, volume);

    // 放下物品音效
    public void PlayObjectDropSound(Vector3 position = default, float volume = 1.0f)
        => PlaySound(clipSO.objectdrop, position, volume);

    // 拿起物品音效
    public void PlayObjectPickSound(Vector3 position = default, float volume = 1.0f)
        => PlaySound(clipSO.objectpick, position, volume);

    // 炉灶音效
    public void PlayStoveSound(Vector3 position = default, float volume = 1.0f)
        => PlaySound(clipSO.stove, position, volume);

    // 垃圾桶音效
    public void PlayTrashSound(Vector3 position = default, float volume = 1.0f)
        => PlaySound(clipSO.trash, position, volume);

    // 着火音效
    public void PlayFireSound(Vector3 position = default, float volume = 1.0f)
        => PlaySound(clipSO.fire, position, volume);

    // =========== 音量控制方法 ===========

    // 设置背景音乐音量
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);

        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.volume = musicVolume;
        }

        PlayerPrefs.SetInt("MusicVolume", Mathf.RoundToInt(musicVolume * 10));
        PlayerPrefs.Save();
    }

    // 设置音效音量
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        PlayerPrefs.SetInt("SFXVolume", Mathf.RoundToInt(sfxVolume * 10));
        PlayerPrefs.Save();
    }

    // 获取背景音乐音量
    public float GetMusicVolume() => musicVolume;

    // 获取音效音量
    public float GetSFXVolume() => sfxVolume;

    // =========== 背景音乐控制方法 ===========

    // 开始播放背景音乐
    public void PlayBackgroundMusic(AudioClip musicClip = null, bool loop = true)
    {
        if (backgroundMusicSource == null)
        {
            Debug.LogError("Background Music AudioSource is not set!");
            return;
        }

        if (musicClip != null)
        {
            backgroundMusicSource.clip = musicClip;
        }

        backgroundMusicSource.loop = loop;
        backgroundMusicSource.volume = musicVolume;

        if (!backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }
    }

    // 停止背景音乐
    public void StopBackgroundMusic()
    {
        if (backgroundMusicSource != null && backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Stop();
        }
    }

    // 暂停背景音乐
    public void PauseBackgroundMusic()
    {
        if (backgroundMusicSource != null && backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Pause();
        }
    }

    // 继续播放背景音乐
    public void ResumeBackgroundMusic()
    {
        if (backgroundMusicSource != null && !backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }
    }

    // 调试方法：检查音量设置
    public void DebugVolumeSettings()
    {
        Debug.Log($"Music Volume: {musicVolume} (UI: {Mathf.RoundToInt(musicVolume * 10)})");
        Debug.Log($"SFX Volume: {sfxVolume} (UI: {Mathf.RoundToInt(sfxVolume * 10)})");
        Debug.Log($"Background Music: {backgroundMusicSource != null && backgroundMusicSource.isPlaying}");
    }
}