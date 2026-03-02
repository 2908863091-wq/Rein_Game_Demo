using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // 添加这个命名空间

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button setButton;
    [SerializeField] private Button escButton;
    [SerializeField] private GameObject UI;

    private SettingUI settingUI;
    private Keyboard keyboard; // 用于新的 Input System

    private void Start()
    {
        UIHide();

        // 初始化键盘输入
        keyboard = Keyboard.current;

        // 查找SettingUI实例
        settingUI = FindObjectOfType<SettingUI>();
        if (settingUI == null)
        {
            Debug.LogError("SettingUI not found in scene!");
        }
        else
        {
            // 设置返回暂停界面的回调
            settingUI.OnBackToPauseMenu += () =>
            {
                // 显示暂停界面
                UIShow();
            };
        }

        continueButton.onClick.AddListener(() =>
        {
            GameManager.Instance.CountiueGame();
        });

        setButton.onClick.AddListener(() =>
        {
            // 显示设置界面，隐藏暂停界面
            if (settingUI != null)
            {
                settingUI.UIShow();
                UIHide();
            }
        });

        escButton.onClick.AddListener(() =>
        {
            // 返回主菜单前保存设置
            SaveSettings();
            Loader.Load(Loader.Scene.start);
        });

        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameContinued += GameManager_OnGameContinued;
    }

    private void Update()
    {
        // 使用新的 Input System 检测 ESC 键
        if (keyboard != null && keyboard.escapeKey.wasPressedThisFrame)
        {
            HandleEscapeKey();
        }
    }

    private void HandleEscapeKey()
    {
        // 如果游戏当前是暂停状态
        if (GameManager.Instance.IsGamePaused())
        {
            // 如果设置界面是打开的，关闭它并显示暂停界面
            if (settingUI != null && settingUI.gameObject.activeSelf)
            {
                // 保存设置
                settingUI.UIHide();
                SaveSettings();
                // 显示暂停界面
                UIShow();
            }
            else
            {
                // 如果暂停界面是打开的，继续游戏
                if (UI.activeSelf)
                {
                    GameManager.Instance.CountiueGame();
                }
            }
        }
    }

    private void SaveSettings()
    {
        // 保存设置到PlayerPrefs或GameManager
        if (settingUI != null)
        {
            PlayerPrefs.SetInt("MusicVolume", settingUI.GetMusicNum());
            PlayerPrefs.SetInt("SFXVolume", settingUI.GetSFXNum());
            PlayerPrefs.Save();
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGamePaused -= GameManager_OnGamePaused;
            GameManager.Instance.OnGameContinued -= GameManager_OnGameContinued;
        }

        // 取消订阅设置界面的回调
        if (settingUI != null)
        {
            settingUI.OnBackToPauseMenu = null;
        }
    }

    private void GameManager_OnGamePaused()
    {
        UIShow();
    }

    private void GameManager_OnGameContinued()
    {
        UIHide();
        if (settingUI != null)
        {
            settingUI.UIHide();
        }
    }

    private void UIShow()
    {
        UI.SetActive(true);
    }

    private void UIHide()
    {
        UI.SetActive(false);
    }
}