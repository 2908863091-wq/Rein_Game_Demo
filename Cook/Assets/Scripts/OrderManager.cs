using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OrderManager : MonoBehaviour
{
    public  static OrderManager instance {  get; private set; }

    public UnityEvent OnSpawn;
    public UnityEvent OnFail;
    public UnityEvent OnSuccess;

    [SerializeField]private MenuListOS menuList;
    [SerializeField]private float orderRate = 5f;
    [SerializeField]private int orderMax = 5;
    private List<MenuOS> orderMenuList = new List<MenuOS>();
    private float orderTimer = 0;
    private bool is_start = false;
    private int orderCount = 0;
    private int orderNum = 0;

    private void Awake()
    {
        instance = this;
        if (OnSpawn == null)OnSpawn = new UnityEvent();
        if (OnFail == null)OnFail = new UnityEvent();
        if (OnSuccess == null)OnSuccess = new UnityEvent();

        OnFail.AddListener(PlayFailSound);
        OnSuccess.AddListener(PlaySuccessSound);
    }
    private void Update()
    {
        if (is_start)
        {
            OrderUpdate();
        }
    }
    private void OrderUpdate()
    {
        orderTimer += Time.deltaTime;
        if (orderTimer > orderRate)
        {
            orderTimer = 0;
            OrderNewMenu();
        }
    }
    private void OrderNewMenu()
    {
        if (orderCount >= orderMax) return;

        int index = Random.Range(0,menuList.list.Count);
        orderMenuList.Add(menuList.list[index]);
        orderCount++;
        OnSpawn?.Invoke();
    }
    public void DeliverMenu(Plate plate)
    {
        MenuOS menuCorrect = null;
        foreach (MenuOS i in orderMenuList) 
        {
            if (Correct(i,plate))
            {
                menuCorrect = i; break;
            }
        
        }
        if (menuCorrect == null)
        {
            Debug.Log("上错菜了，杂鱼！");
            OnFail?.Invoke();
        }
        else
        {
            orderMenuList.Remove(menuCorrect);
            orderCount--;
            orderNum++;
            Debug.Log("上菜成功喵");
            OnSpawn?.Invoke();
            OnSuccess?.Invoke();

        }
    }
    public bool Correct(MenuOS menuOS,Plate plate) 
    {
        List<KitchenObject>list1 = menuOS.kitchenObjectList;
        List<KitchenObject>list2 = plate.GetPlateList();

        if(list1.Count != list2.Count) return false;

        foreach (KitchenObject kitchenObject in list1)
        {
            if (list2.Contains(kitchenObject) == false)
            {
                return false;
            }
        }
        return true;
    }
    public List<MenuOS> GetList()
    {
        return orderMenuList;
    }
    private void PlaySuccessSound()
    {
        if (MusicManager.Instance != null)
            MusicManager.Instance.PlaySuccessSound();
    }

    private void PlayFailSound()
    {
        if (MusicManager.Instance != null)
            MusicManager.Instance.PlayFailSound();
    }
    public void OrderStart()
    {
        is_start = true;
    }
    public void OrderEnd()
    {
        is_start = false;
    }
    public int GetNum() 
    {
        return orderNum;
    }
}
