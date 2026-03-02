using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveFX : MonoBehaviour
{
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject FX;

    public void Show()
    {
        fire.SetActive(true);
        FX.SetActive(true);
    }
    public void Hide()
    {
        fire.SetActive(false);
        FX.SetActive(false);
    }

}
