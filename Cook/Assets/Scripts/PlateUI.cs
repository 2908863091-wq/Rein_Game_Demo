using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateUI : MonoBehaviour
{
    [SerializeField]private Image foodImage;

    public void Show(Sprite sprite)
    {
        gameObject.SetActive(true);
        foodImage.sprite = sprite;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
