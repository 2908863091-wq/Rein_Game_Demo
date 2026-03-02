using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderUI : MonoBehaviour
{
    [SerializeField] private Transform menuParent;
    [SerializeField] private MenuListUI menuListUI;
    [SerializeField] private OrderManager orderManager;

    private void Start()
    {
        menuListUI.gameObject.SetActive(false);
        orderManager.OnSpawn.AddListener(UpdateUI);
    }
    private void OnDestroy()
    {        
        if (orderManager != null)
        {
            orderManager.OnSpawn.RemoveListener(UpdateUI);
        }
    }
    private void UpdateUI()
    {
        foreach (Transform child in menuParent)
        {
            if (child != menuParent.transform) 
            { 
                Destroy(child.gameObject);
            }
        }
        List<MenuOS>menuList = OrderManager.instance.GetList();
        foreach (MenuOS menuOS in menuList) 
        {
            MenuListUI ui = GameObject.Instantiate(menuListUI);
            ui.transform.SetParent(menuParent);
            ui.gameObject.SetActive(true);
            ui.UpdateUI(menuOS);
        }
    }
}
