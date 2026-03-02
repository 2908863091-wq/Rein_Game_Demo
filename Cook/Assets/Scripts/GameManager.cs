using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI countDown;
    [SerializeField] private TextMeshProUGUI TimeUI;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private GameObject teach;
    private State state;
    private float waitingToStartTimer = 3f;
    private float CountDownToStartTimer = 3f;
    private float GamePlayingTimer = 180f;
    private bool isGamePause = false;

    public event System.Action OnGamePaused;
    public event System.Action OnGameContinued;
    private void Awake()
    {
        Instance = this;
        teach.SetActive(false);
        TurnTowaitingToStart();
    }
    private void Start()
    {
        GameInput.Instance.Theworld += Instance_Theworld;        
    }
    private void Instance_Theworld(object sender, System.EventArgs e)
    {
        if (isGamePause == false)
        {
            PauseGame();
        }
        else
        {
            CountiueGame();
        }
    }

    private void PauseGame()
    {
        if (state == State.GamePlaying)
        {
            Time.timeScale = 0;
            isGamePause = true;
            OnGamePaused?.Invoke(); 
        }
    }

    public void CountiueGame()
    {
        if (isGamePause == true)
        {
            Time.timeScale = 1;
            isGamePause = false;
            OnGameContinued?.Invoke();
        }
    }


    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartGame();
        }
        switch (state) 
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if( waitingToStartTimer <= 0f)
                {
                    TurnCountDownToStart();
                }
                break;
            case State.CountDownToStart:
                CountDownToStartTimer -= Time.deltaTime;
                UpdateText();
                if (CountDownToStartTimer <= 0f)
                {
                    TurnToGamePlaying();
                }
                break ;
            case State.GamePlaying:
                GamePlayingTimer -= Time.deltaTime;
                UpdateTimeText();
                if (GamePlayingTimer <= 0f)
                {
                    TurnToGameOver();
                }
                break;
            case State.GameOver:
                break;
            default:
                break;
        }
    }
    private void RestartGame()
    {
        
    }
    private void TurnTowaitingToStart()
    {
        state = State.WaitingToStart;
        DisablePlayer();
        teach.SetActive(true);
        HideText();
        HideTimeText();
    }
    private void TurnCountDownToStart()
    {
        state = State.CountDownToStart;
        DisablePlayer();
        ShowText();
        HideTimeText();
        teach.SetActive(false);
    }
    private void TurnToGamePlaying()
    {
        state = State.GamePlaying;
        EnablePlayer();
        OrderManager.instance.OrderStart();
        HideText();
        ShowTimeText();
    }
    private void TurnToGameOver()
    {
        state = State.GameOver;
        gameOverUI.Show();
        gameOverUI.SetNumber(Mathf.CeilToInt(OrderManager.instance.GetNum()).ToString());
        OrderManager.instance.OrderEnd();
        DisablePlayer();
        HideTimeText();
    }
    private void DisablePlayer()
    {
        player.enabled = false;
    }
    private void EnablePlayer()
    {
        player.enabled = true;
    }
    private void HideText()
    {
        countDown.enabled = false;
    }
    private void ShowText()
    {
        countDown.enabled = true;
    }
    private void UpdateText()
    {
        countDown.text = Mathf.CeilToInt(CountDownToStartTimer).ToString();
    }
    private void UpdateTimeText()
    {
        TimeUI.text = Mathf.CeilToInt(GamePlayingTimer).ToString();
    }
    private void HideTimeText()
    {
        TimeUI.enabled = false;
    }
    private void ShowTimeText()
    {
        TimeUI.enabled = true;
    }
    public bool IsGamePaused()
    {
        return isGamePause;
    }
}
