using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KitchenandPlayer : MonoBehaviour
{
    [SerializeField] private Transform top_point;

    private Kitchen kitchen;
    public static UnityEvent OnPick;
    private void Awake()
    {
        if (OnPick == null)
        {
            OnPick = new UnityEvent();
            OnPick.AddListener(PlayPickSound);
        }
    }
    public void SetKitchen(Kitchen kitchen)
    {
        this.kitchen = kitchen;
        kitchen.transform.localPosition = Vector3.zero;
    }
    public Kitchen GetKitchen()
    {
        return kitchen;
    }
    public void TransferFood(KitchenandPlayer source, KitchenandPlayer target)
    {
        if (source.GetKitchen() == null)
        {
            Debug.LogWarning("Null!!!ERROR!!!");
            return;
        }
        if (target.GetKitchen() != null)
        {
            Debug.LogWarning("Full!!!ERROR!!!");
            return;
        }
        target.AddKitchen(source.GetKitchen());
        source.ClearKitchen();
        OnPick?.Invoke();
    }
    public void AddKitchen(Kitchen kitchen)
    {
        kitchen.transform.SetParent(top_point);
        kitchen.transform.localPosition = Vector3.zero;
        this.kitchen = kitchen;
    }
    public void ClearKitchen()
    {
        this.kitchen = null;
    }
    public Transform GetPoint()
    {
        return top_point;
    }
    public bool FoodARU()
    {
        return kitchen != null;
    }
    public void DestroyFood()
    {
        Destroy(kitchen.gameObject);
        ClearKitchen();
    }
    public void CreateKitchen(GameObject kitchenObject)
    {
        if (kitchenObject == null)
        {
            Debug.LogError("CreateKitchen: kitchenObject 为空");
            return;
        }

        // 实例化物体
        GameObject newObj = Instantiate(kitchenObject, GetPoint());

        // 获取 Kitchen 组件
        Kitchen kitchen = newObj.GetComponent<Kitchen>();

        if (kitchen != null)
        {
            // 设置到当前对象
            SetKitchen(kitchen);
        }
        else
        {
            Debug.LogError($"生成的物体 {newObj.name} 没有 Kitchen 组件！");
        }

    }
    private void PlayPickSound()
    {
        if (MusicManager.Instance != null)
            MusicManager.Instance.PlayObjectPickSound();
    }
}
