using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour
{
    [SerializeField] private KitchenObject kitchenObject;

    public KitchenObject KitchenObject()
    {
        return kitchenObject;
    }
}
