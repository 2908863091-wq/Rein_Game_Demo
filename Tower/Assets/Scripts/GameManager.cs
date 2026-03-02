using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private GamePasueUI gamePasueUI;
    [SerializeField] private InputActionReference pauseAction; // 新输入系统的暂停动作

    private bool _isPaused = false;      // 当前是否处于暂停状态
    private bool _gameOver = false;       // 游戏是否已结束（胜利/失败）

    private void Awake()
    {
        // 单例模式，确保只有一个实例
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // 可选：DontDestroyOnLoad(gameObject); // 如果需要跨场景保留
    }

    private void OnEnable()
    {
        // 订阅输入事件
        if (pauseAction != null)
            pauseAction.action.performed += OnPausePerformed;
    }

    private void OnDisable()
    {
        // 取消订阅
        if (pauseAction != null)
            pauseAction.action.performed -= OnPausePerformed;
    }

    // 输入回调：按下暂停键时触发
    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        TogglePause();
    }

    /// <summary>
    /// 切换暂停状态（由输入调用）
    /// </summary>
    public void TogglePause()
    {
        if (_gameOver) return; // 游戏结束时不能暂停/继续

        if (_isPaused)
            Continue();
        else
            Pause();
    }

    /// <summary>
    /// 强制暂停游戏（如果游戏未结束且未暂停）
    /// </summary>
    public void Pause()
    {
        if (_gameOver || _isPaused) return;

        PauseGame();
    }

    /// <summary>
    /// 强制继续游戏（如果游戏未结束且已暂停）
    /// </summary>
    public void Continue()
    {
        if (_gameOver || !_isPaused) return;

        ContinueGame();
    }

    // 内部暂停逻辑
    private void PauseGame()
    {
        _isPaused = true;
        Time.timeScale = 0f;
        if (gamePasueUI != null)
            gamePasueUI.Show();
    }

    // 内部继续逻辑
    private void ContinueGame()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        if (gamePasueUI != null)
            gamePasueUI.Hide();
    }

    /// <summary>
    /// 游戏胜利
    /// </summary>
    public void Win()
    {
        if (_gameOver) return;
        _gameOver = true;
        Time.timeScale = 0f;
        if (gameOverUI != null)
            gameOverUI.SetText("You Win");
    }

    /// <summary>
    /// 游戏失败
    /// </summary>
    public void Lose()
    {
        if (_gameOver) return;
        _gameOver = true;
        Time.timeScale = 0f;
        if (gameOverUI != null)
            gameOverUI.SetText("You Lose");
    }

    /// <summary>
    /// 重新开始游戏（重新加载当前场景）
    /// </summary>
    public void ReStart()
    {
        // 重置时间缩放
        Time.timeScale = 1f;
        // 重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}