using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shang_cai : BaseBlock
{
    public override void InterAct(Player player)
    {
        if (player.FoodARU() && player.GetKitchen().TryGetComponent<Plate>(out Plate plate))
        {
            OrderManager.instance.DeliverMenu(plate);
            player.DestroyFood();
        }
    }
}
