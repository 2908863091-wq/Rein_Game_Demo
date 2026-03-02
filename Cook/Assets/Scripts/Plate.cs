using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate :Kitchen
{
    [SerializeField] List<KitchenObject> allowFoodList;
    [SerializeField] private Hamburger hamburger;
    [SerializeField] private PlateUIGroup plateUIGroup;
    private List<KitchenObject> plateFoodList = new List<KitchenObject>();

    public bool AddKitchen(KitchenObject kitchenObject)
    {
        if (plateFoodList.Contains(kitchenObject))
        {
            return false;
        }
        if (allowFoodList.Contains(kitchenObject) == false)
        {
            return false;
        }
        hamburger.ShowKitchenObject(kitchenObject);
        plateUIGroup.ShowUI(kitchenObject);
        plateFoodList.Add(kitchenObject);
        return true;
    }
    public List<KitchenObject> GetPlateList()
    {
        return plateFoodList;
    }
}
