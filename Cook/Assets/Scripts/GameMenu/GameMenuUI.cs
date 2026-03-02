using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button setButton;
    [SerializeField] private Button escButton;

    private SettingUI settingUI; // 添加这行

    private void Start()
    {
        // 查找SettingUI实例
        settingUI = FindObjectOfType<SettingUI>();
        if (settingUI == null)
        {
            Debug.LogError("SettingUI not found in scene!");
        }

        startButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.game);
        });

        setButton.onClick.AddListener(() =>
        {
            // 显示设置界面
            if (settingUI != null)
            {
                settingUI.UIShow();
            }
        });

        escButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}