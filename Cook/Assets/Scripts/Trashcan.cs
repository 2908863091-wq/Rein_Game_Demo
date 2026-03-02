using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trashcan : BaseBlock
{
    public static UnityEvent OnTrash;
    private void Awake()
    {
        if (OnTrash == null)
        {
            OnTrash = new UnityEvent();
            OnTrash.AddListener(PlayOnTrashSound);
        }
    }
    public override void InterAct(Player player)
    {
        if (player.FoodARU())
        {
            player.DestroyFood();
            OnTrash?.Invoke();
        }
    }
    private void PlayOnTrashSound()
    {
        if (MusicManager.Instance != null)
            MusicManager.Instance.PlayTrashSound();
    }
}
