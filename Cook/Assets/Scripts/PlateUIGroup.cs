using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateUIGroup : MonoBehaviour
{
    [SerializeField] private PlateUI PlateUI;

    private void Start()
    {
        PlateUI.Hide();
    }
    public void ShowUI(KitchenObject kitchenObject)
    {
        PlateUI newPlateUI = GameObject.Instantiate(PlateUI,transform);
        newPlateUI.Show(kitchenObject.sprite);
    }
}
