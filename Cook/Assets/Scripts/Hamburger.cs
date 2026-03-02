using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hamburger : MonoBehaviour
{
    [Serializable]
    public class KitchenObject_Model
    {
        public KitchenObject kitchenObject;
        public GameObject model;
    }
    [SerializeField]private List<KitchenObject_Model> models;
    public void ShowKitchenObject(KitchenObject kitchenObject)
    {
        foreach (KitchenObject_Model item in models) 
        {
            if (item.kitchenObject == kitchenObject)
            {
                item.model.SetActive(true);return;
            }
        }
    }
}
