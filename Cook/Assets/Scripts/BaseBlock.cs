using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBlock : KitchenandPlayer
{
    [SerializeField] private GameObject Selected;

    private void Start()
    {
        if (Selected != null)
        {
            Selected.SetActive(false);
        }
    }
    public virtual void InterAct(Player player)
    {
        Debug.LogWarning("…µ±∆√ª÷ÿ–¥");
    }
    public virtual void ExtraInterAct(Player player)
    {

    }
    public void SetTrue()
    {
        Selected.SetActive(true);
    }
    public void SetFalse()
    {
        Selected.SetActive(false);
    }

}
