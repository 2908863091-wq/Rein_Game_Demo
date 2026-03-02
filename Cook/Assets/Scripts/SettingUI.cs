using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private Button musicAdd;
    [SerializeField] private Button musicSub;
    [SerializeField] private Button SFXAdd;
    [SerializeField] private Button SFXSub;
    [SerializeField] private Button escButton;
    [SerializeField] private Text musicVloume;
    [SerializeField] private Text SFXVloume;
    [SerializeField] private GameObject UI;
    private int musicNum = 8;
    private int SFXNum = 8;

    // 音量变化事件
    public System.Action<int> OnMusicVolumeChanged;
    public System.Action<int> OnSFXVolumeChanged;

    // 添加返回暂停界面的回调
    public System.Action OnBackToPauseMenu;

    private void Start()
    {
        UIHide();

        // 初始化按钮事件
        musicAdd.onClick.AddListener(AddMusicVolume);
        musicSub.onClick.AddListener(SubMusicVolume);
        SFXAdd.onClick.AddListener(AddSFXVolume);
        SFXSub.onClick.AddListener(SubSFXVolume);
        escButton.onClick.AddListener(OnEscButtonClicked);

        // 加载保存的设置
        LoadSettings();
        UpdateUI();

        // 初始化时通知MusicManager
        UpdateMusicManager();
    }

    private void AddMusicVolume()
    {
        if (musicNum < 10)
        {
            musicNum++;
            UpdateMusicUI();
            OnMusicVolumeChanged?.Invoke(musicNum);
            UpdateMusicManager();
        }
    }

    private void SubMusicVolume()
    {
        if (musicNum > 0)
        {
            musicNum--;
            UpdateMusicUI();
            OnMusicVolumeChanged?.Invoke(musicNum);
            UpdateMusicManager();
        }
    }

    private void AddSFXVolume()
    {
        if (SFXNum < 10)
        {
            SFXNum++;
            UpdateSFXUI();
            OnSFXVolumeChanged?.Invoke(SFXNum);
            UpdateMusicManager();
        }
    }

    private void SubSFXVolume()
    {
        if (SFXNum > 0)
        {
            SFXNum--;
            UpdateSFXUI();
            OnSFXVolumeChanged?.Invoke(SFXNum);
            UpdateMusicManager();
        }
    }

    private void OnEscButtonClicked()
    {
        SaveSettings();
        UIHide();

        // 只有在游戏场景中才触发返回暂停菜单回调
        // 主场景中只需要关闭设置界面
        if (GameManager.Instance != null)
        {
            OnBackToPauseMenu?.Invoke();
        }
    }

    public void UIShow()
    {
        UI.SetActive(true);
        SyncVolumeSettings();
    }

    public void UIHide()
    {
        UI.SetActive(false);
    }

    private void UpdateUI()
    {
        UpdateMusicUI();
        UpdateSFXUI();
    }

    private void UpdateMusicUI()
    {
        musicVloume.text = "音乐：" + musicNum.ToString();
    }

    private void UpdateSFXUI()
    {
        SFXVloume.text = "音效：" + SFXNum.ToString();
    }

    private void UpdateMusicManager()
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.SetMusicVolume(musicNum / 10f);
            MusicManager.Instance.SetSFXVolume(SFXNum / 10f);
        }
    }

    private void SyncVolumeSettings()
    {
        if (MusicManager.Instance != null)
        {
            musicNum = Mathf.RoundToInt(MusicManager.Instance.GetMusicVolume() * 10);
            SFXNum = Mathf.RoundToInt(MusicManager.Instance.GetSFXVolume() * 10);
            UpdateUI();
        }
    }

    public int GetMusicNum() => musicNum;
    public int GetSFXNum() => SFXNum;

    public void SetMusicVolume(int volume)
    {
        musicNum = Mathf.Clamp(volume, 0, 10);
        UpdateMusicUI();
        UpdateMusicManager();
    }

    public void SetSFXVolume(int volume)
    {
        SFXNum = Mathf.Clamp(volume, 0, 10);
        UpdateSFXUI();
        UpdateMusicManager();
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("MusicVolume", musicNum);
        PlayerPrefs.SetInt("SFXVolume", SFXNum);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            int musicVolume = PlayerPrefs.GetInt("MusicVolume");
            SetMusicVolume(musicVolume);
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            int sfxVolume = PlayerPrefs.GetInt("SFXVolume");
            SetSFXVolume(sfxVolume);
        }
    }
}