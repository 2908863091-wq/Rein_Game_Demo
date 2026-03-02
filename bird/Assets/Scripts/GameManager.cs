using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    public Text score_text;
    private int myScore = 0;
    private Bird[] birdsList;
    private int index = -1;
    private int pig_num_die;
    private int pig_num_max;
    private bool is_win = false;
    public GameObject win;
    public GameObject lose;

    private FollowTarget cameraFollowTarget;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        lose.SetActive(false);
        win.SetActive(false);
        is_win = false;
        birdsList = FindObjectsByType<Bird>(FindObjectsSortMode.None);
        pig_num_max = FindObjectsByType<Pig>(FindObjectsSortMode.None).Length;
        cameraFollowTarget = Camera.main.GetComponent<FollowTarget>();

        NextBird();
    }

    // Update is called once per frame
    void Update()
    {
        score_text.text = myScore.ToString();

        if (Input.GetKeyDown(KeyCode.R))
        {
            RePlay();
        }
    }
    public void NextBird()
    {
        index++;
        while (index < birdsList.Length && birdsList[index] == null)
        {
            index++;
        }
        if (index >= birdsList.Length)
        {
            GameOver_lose();
            return;
        }

        birdsList[index].GoStage(Shooter.Instance.getCenterPosition());
        cameraFollowTarget.SetTarget(birdsList[index].transform);
    }
    public void Pigdie()
    {
        pig_num_die++;
        if (pig_num_die >= pig_num_max)
        {
            GameOver_win();
        }
    }
    private void GameOver_lose()
    {
        if (is_win == false)
        {
            Debug.Log("≤À±∆");
            lose.SetActive(true);
            
        }
    }

    private void GameOver_win()
    {
        Debug.Log("”Æ¡À£°");
        is_win = true;
        win.SetActive(true);
    }

    private void RePlay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void AddScore(int num)
    {
        myScore += num;
    }
}
