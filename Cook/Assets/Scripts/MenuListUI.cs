using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuListUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI my_name;
    [SerializeField] private Transform parent;
    [SerializeField] private Image image;

    private void Start()
    {
        image.gameObject.SetActive(false);
    }
    public void UpdateUI(MenuOS menu)
    {
        my_name.text = menu.menuName;
        foreach (KitchenObject kitchenObject in menu.kitchenObjectList)
        {
            Image newIcon = GameObject.Instantiate(image);
            newIcon.name = kitchenObject.objectName_ima;
            newIcon.transform.SetParent(parent, false);
            newIcon.sprite = kitchenObject.sprite;
            newIcon.gameObject.SetActive(true);
        }
    }
}
