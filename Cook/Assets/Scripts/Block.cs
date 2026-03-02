using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Block : BaseBlock
{
    public override void InterAct(Player player)
    {       
            if (player.FoodARU())
            {
                bool playerHasPlate = player.GetKitchen().TryGetComponent<Plate>(out Plate plate);                

                if (playerHasPlate)
                    {
                        if (FoodARU() == false)
                        {
                            TransferFood(player, this);
                        }
                        else
                        {
                            bool is_success = plate.AddKitchen(GetKitchen().KitchenObject());
                            if (is_success)
                            {
                                DestroyFood();
                            }
                        }
                    }
                    else
                    {
                        if (FoodARU() == false)
                        {
                            TransferFood(player, this);
                        }
                    else
                    {
                        if (GetKitchen().TryGetComponent<Plate>(out plate))
                        {
                            KitchenObject playerFood = player.GetKitchen().KitchenObject();
                            if (playerFood != null && plate.AddKitchen(playerFood))
                            {
                                player.DestroyFood();
                            }
                        }
                    }

            }
                
            }
            else
            {
                if (FoodARU() == true)
                {
                    TransferFood(this, player);
                }

            }
        



    }

}
