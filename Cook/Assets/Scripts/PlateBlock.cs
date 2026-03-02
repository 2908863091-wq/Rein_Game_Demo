using System.Collections.Generic;
using UnityEngine;

public class PlateBlock : BaseBlock
{
    [SerializeField] private float spawnTime = 3;
    [SerializeField] private int plateMax = 3;
    [SerializeField] private KitchenObject plate;

    private float timer = 0;
    private List<Kitchen> plateList = new List<Kitchen>();

    private void Update()
    {
        if (plateList.Count < plateMax)
        {
            timer += Time.deltaTime;
            if (timer >= spawnTime)
            {
                SpawnPlate();
            }
        }
    }

    public override void InterAct(Player player)
    {
        // 情况1：玩家手上是空的 - 给玩家一个空碟子
        if (!player.FoodARU())
        {
            if (plateList.Count > 0)
            {
                Kitchen topPlate = plateList[plateList.Count - 1];
                player.AddKitchen(topPlate);
                plateList.RemoveAt(plateList.Count - 1);
            }
        }
        // 情况2：玩家手上有食物，碟子台上有碟子 - 把食物放到最上面的碟子里
        else if (player.FoodARU() && plateList.Count > 0)
        {
            Kitchen topPlate = plateList[plateList.Count - 1];

            // 检查这个碟子是不是Plate组件
            if (topPlate.TryGetComponent<Plate>(out Plate plateComponent))
            {
                KitchenObject playerFood = player.GetKitchen().KitchenObject();

                if (playerFood != null && plateComponent.AddKitchen(playerFood))
                {
                    // 成功放入，玩家手上的食物消失
                    player.DestroyFood();
                }
            }
        }
        // 情况3：玩家手上有碟子，碟子台是空的 - 把碟子放在碟子台上
        else if (player.FoodARU() && plateList.Count < plateMax)
        {
            Kitchen playerKitchen = player.GetKitchen();
            if (playerKitchen.TryGetComponent<Plate>(out Plate _))
            {
                // 把玩家手上的碟子放到碟子台上
                playerKitchen.transform.SetParent(GetPoint());
                playerKitchen.transform.localPosition = Vector3.zero + Vector3.up * 0.1f * plateList.Count;
                plateList.Add(playerKitchen);
                player.ClearKitchen();
            }
        }
    }

    private void SpawnPlate()
    {
        timer = 0;
        CreatePlate(plate.prefab);
    }

    public void CreatePlate(GameObject kitchenObject)
    {
        if (kitchenObject == null) return;

        GameObject newObj = Instantiate(kitchenObject, GetPoint());
        Kitchen kitchen = newObj.GetComponent<Kitchen>();

        if (kitchen != null)
        {
            if (plateList.Count >= plateMax) return;

            kitchen.transform.localPosition = Vector3.zero + Vector3.up * 0.1f * plateList.Count;
            plateList.Add(kitchen);
        }
    }
}