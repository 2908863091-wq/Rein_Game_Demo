using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    [SerializeField] private TurretData standard;
    [SerializeField] private TurretData missile;
    [SerializeField] private TurretData laser;


    [SerializeField] private TextMeshProUGUI moneyUI;

    [SerializeField] private int startMoney = 100;

    public TurretData selectedTurretData;
    public State state;

    private Animator moneyAni;
    private AudioSource moneyAudio;
    private int money;

    public enum State
    {
        BuildStandard,  // 建造标准炮塔
        BuildMissile,   // 建造导弹炮塔
        BuildLaser,     // 建造激光炮塔
        Break,          // 拆除模式
        Upgrade         // 升级模式
    }
    private void Awake()
    {
        Instance = this;
        moneyAni = moneyUI.GetComponent<Animator>();
        moneyAudio = moneyUI.GetComponentInChildren<AudioSource>();
    }
    private void Start()
    {
        AddMoney(startMoney);
    }
    public void OnStandard(bool isOn)
    {
        if (isOn)
        {
            state = State.BuildStandard;
            selectedTurretData = standard;
        }
    }
    public void OnMissile(bool isOn)
    {
        if (isOn)
        {
            state = State.BuildMissile;
            selectedTurretData = missile;
        }
    }
    public void OnLazer(bool isOn)
    {
        if (isOn)
        {
            state = State.BuildLaser;
            selectedTurretData = laser;
        }
    }
    public void OnBreak(bool isOn)
    {
        if (isOn)
        {
            state = State.Break;
            selectedTurretData = null;
        }
    }
    public void LevelUP(bool isOn)
    {
        if (isOn)
        {
            state = State.Upgrade;
            selectedTurretData = null;
        }
    }

    public void AddMoney(int num)
    {
        money += num;
        moneyUI.text = "¥:" + money.ToString();
    }
    public int GetMoney()
    {
        return money;
    }
    public void MoneyAnimation()
    {
        moneyAni.SetTrigger("lighting");
        moneyAudio.Play();
    }
}

